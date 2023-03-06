using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemForMission : MonoBehaviour
{
    [SerializeField] string MissionID;

    int MissionState;
    private void Start()
    {
        MissionState =
        new Account_MissionDAO().GetAccountMissionByMissionID(AccountManager.AccountID, MissionID).State;

        if (MissionState == 1)
        {
            gameObject.SetActive(false);
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
