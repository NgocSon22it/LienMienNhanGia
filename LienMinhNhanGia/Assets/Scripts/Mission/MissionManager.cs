using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class MissionManager : MonoBehaviour
{

    [Header("Instance")]
    public static MissionManager Instance;

    [SerializeField] GameObject MissionItem;
    [SerializeField] Transform Content;

    List<AccountMissionEntity> ListAccountMission = new List<AccountMissionEntity>();


    public int MissionCount;

    [Header("DAO")]
    [SerializeField] GameObject DAOManager;
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
        ListAccountMission = DAOManager.GetComponent<Account_MissionDAO>().GetAllMissionForAccount(AccountManager.AccountID);
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (AccountMissionEntity accountMission in ListAccountMission)
        {
            MissionEntity mission = DAOManager.GetComponent<MissionDAO>().GetMissionbyId(accountMission.MissionID);
            Instantiate(MissionItem, Content).GetComponent<MissionItem>().SetUp(mission, accountMission.Current, accountMission.State);
        }
    }

    public void IncreaseCurrentMission(string MissionID)
    {
        DAOManager.GetComponent<Account_MissionDAO>().UpdateAccountMissionCurrent(AccountManager.AccountID, MissionID);
        LoadMissionList();
    }

    public void ClaimRewardSelectedMission(MissionEntity missionEntity)
    {

        DAOManager.GetComponent<Account_MissionDAO>().UpdateAccountMissionState(AccountManager.AccountID, missionEntity.MissionID, 1);
        LevelManager.Instance.AddExperience(missionEntity.ExperienceBonus);
        LoadMissionList();

    }
}
