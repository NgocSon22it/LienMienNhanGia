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

    List<AccountMissionEntity> List = new List<AccountMissionEntity>();

    public int Gold;
    public int Exp;

    [Header("DAO")]
    MissionDAO missionDAO;
    Account_MissionDAO account_MissionDAO;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        missionDAO = GetComponent<MissionDAO>();
        account_MissionDAO = GetComponent<Account_MissionDAO>();
        LoadMissionList();
    }


    public void LoadMissionList()
    {
        List = account_MissionDAO.GetAllMissionForAccount(2);
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (AccountMissionEntity accountMission in List)
        {
            MissionEntity mission = missionDAO.GetMissionbyId(accountMission.MissionID);
            Instantiate(MissionItem, Content).GetComponent<MissionItem>().SetUp(mission, accountMission.Current, accountMission.State);
        }
    }

    public void AddCurrentAmountMission(int MissionID)
    {
        account_MissionDAO.UpdateAccountMissionCurrent(2, MissionID);
        LoadMissionList();
    }

    public void ClaimRewardSelectedMission(MissionEntity missionEntity)
    {
        Gold += missionEntity.CoinBonus;
        Exp  += missionEntity.ExperienceBonus;
        account_MissionDAO.UpdateAccountMissionState(2, missionEntity.MissionID, 1);
        LoadMissionList();
    }
}
