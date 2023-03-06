using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    [Header("Instance")]
    public static MissionManager Instance;

    [SerializeField] GameObject MissionItem;
    [SerializeField] Transform Content;

    List<AccountMissionEntity> ListAccountMission = new List<AccountMissionEntity>();


    public int MissionCount;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadMissionList();
    }


    public void LoadMissionList()
    {
        ListAccountMission = new Account_MissionDAO().GetAllMissionForAccount(AccountManager.AccountID);
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (AccountMissionEntity accountMission in ListAccountMission)
        {
            MissionEntity mission = new MissionDAO().GetMissionbyId(accountMission.MissionID);
            Instantiate(MissionItem, Content).GetComponent<MissionItem>().SetUp(mission, accountMission.Current, accountMission.State);
        }
    }

    public void IncreaseCurrentMission(string MissionID)
    {
        new Account_MissionDAO().UpdateAccountMissionCurrent(AccountManager.AccountID, MissionID);
        LoadMissionList();
    }

    public void ClaimRewardSelectedMission(MissionEntity missionEntity)
    {

        new Account_MissionDAO().UpdateAccountMissionState(AccountManager.AccountID, missionEntity.MissionID, 1);
        LevelManager.Instance.AddExperience(missionEntity.ExperienceBonus);
        AccountManager.Account.Coin += missionEntity.CoinBonus;
        LoadMissionList();

    }
}
