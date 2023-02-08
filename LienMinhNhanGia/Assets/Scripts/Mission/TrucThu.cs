using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrucThu : MonoBehaviour
{
    string MissionID = "Map1_NV";   

    [SerializeField] GameObject MissionPanel;
    [SerializeField] GameObject DAOManager;

    BoxCollider2D boxCollider2D;

    List<AccountMissionEntity> ListAccountMission;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        ListAccountMission = DAOManager.GetComponent<Account_MissionDAO>().GetAllMissionForAccount(1);
        if(ListAccountMission.Count > 0)
        {
            foreach(AccountMissionEntity missionEntity in ListAccountMission)
            {
                if(missionEntity.MissionID.Equals(MissionID + "1")) 
                {
                    boxCollider2D.enabled = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void AddMissionToAccount()
    {
        for(int i = 1; i <= 3; i++)
        {
            DAOManager.GetComponent<Account_MissionDAO>().AddMissionToAccount(1, MissionID + i);
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
