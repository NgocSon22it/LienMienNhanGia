using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrucThu : MonoBehaviour
{
    [SerializeField] List<string> ListMissionID;   
    [SerializeField] GameObject MissionPanel;
    [SerializeField] GameObject DAOManager;

    BoxCollider2D boxCollider2D;

    List<AccountMissionEntity> ListAccountMission;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        ListAccountMission = DAOManager.GetComponent<Account_MissionDAO>().GetAllMissionForAccount(AccountManager.AccountID);
        if(ListAccountMission.Count > 0)
        {
            foreach(AccountMissionEntity missionEntity in ListAccountMission)
            {
                if(ListMissionID.Contains(missionEntity.MissionID)) 
                {
                    boxCollider2D.enabled = false;
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
            DAOManager.GetComponent<Account_MissionDAO>().AddMissionToAccount(1, a);
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
