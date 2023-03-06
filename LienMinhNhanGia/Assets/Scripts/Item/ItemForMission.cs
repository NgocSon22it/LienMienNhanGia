using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemForMission : MonoBehaviour
{
    [SerializeField] string MissionID;

    int MissionState;
    AccountMissionEntity accountMissionEntity;
    MissionEntity missionEntity;
    private void Start()
    {
        accountMissionEntity =
        new Account_MissionDAO().GetAccountMissionByMissionID(AccountManager.AccountID, MissionID);

        if (accountMissionEntity != null)
        {
            missionEntity = new MissionDAO().GetMissionbyId(accountMissionEntity.MissionID);

            MissionState = accountMissionEntity.State;
            if (MissionState == 1 || (accountMissionEntity.Current == missionEntity.Target))
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MissionManager.Instance.IncreaseCurrentMission(MissionID);
            MissionManager.Instance.MissionCount++;
            gameObject.SetActive(false);
        }
    }

}
