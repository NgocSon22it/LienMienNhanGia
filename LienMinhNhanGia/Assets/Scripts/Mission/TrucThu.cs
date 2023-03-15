using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrucThu : MonoBehaviour
{
    [SerializeField] List<string> ListMissionID;   
    [SerializeField] GameObject MissionPanel;

    BoxCollider2D boxCollider2D;

    private void Start()
    {
        if(AccountManager.ListAccountMission.Count > 0)
        {
            foreach(AccountMissionEntity missionEntity in AccountManager.ListAccountMission)
            {
                if(ListMissionID.Contains(missionEntity.MissionID)) 
                {
                    //boxCollider2D.enabled = false;
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    public void AddMissionToAccount()
    {
        foreach(string a in  ListMissionID)
        {
            new Account_MissionDAO().AddMissionToAccount(AccountManager.AccountID, a);
        }
        MissionManager.Instance.LoadMissionList();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            AddMissionToAccount();
            UIManager.Instance.ControlPauseGame(MissionPanel, KeyCode.E);
            Destroy(gameObject);
        }
    }
}
