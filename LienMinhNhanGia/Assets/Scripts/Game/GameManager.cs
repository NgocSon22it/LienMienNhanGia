using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    OfflinePlayer Player;

    [Header("CheckPoint")]
    CheckPoint checkPoint;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<OfflinePlayer>();
        checkPoint = GetSaveCheckPointByID(new AccountDAO().GetAccountByID(AccountManager.AccountID).CheckPoint);
        Debug.Log(checkPoint.transform.position);
        Player.transform.position = checkPoint.transform.position;
    }

    public CheckPoint GetSaveCheckPointByID(string CheckPointID)
    {
        CheckPoint checkPoint = null;
        CheckPoint[] allCheckPoint = GameObject.FindObjectsOfType<CheckPoint>();

        foreach (CheckPoint currentCheckPoint in allCheckPoint)
        {
            if (currentCheckPoint.GetCheckPointID().Equals(CheckPointID))
            {
                checkPoint = currentCheckPoint;
            }
        }
        return checkPoint;
    }

}
