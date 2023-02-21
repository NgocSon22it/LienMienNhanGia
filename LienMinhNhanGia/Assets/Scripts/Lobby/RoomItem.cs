using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] TMP_Text RoomNameTxt;
    [SerializeField] TMP_Text NumberPlayerTxt;
    [SerializeField] TMP_Text BossNameTxt;

    [SerializeField] GameObject LockIcon;



    RoomInfo Roominformation;

    public void SetUp(RoomInfo _Roominformation)
    {
        Roominformation = _Roominformation;
        RoomNameTxt.text = "Phòng " + Roominformation.Name + " của " + Roominformation.CustomProperties["Creator"].ToString();
        NumberPlayerTxt.text = Roominformation.PlayerCount + " / " + Roominformation.MaxPlayers;
        BossNameTxt.text = Roominformation.CustomProperties["BossName"].ToString();

        if (Roominformation.CustomProperties.ContainsKey("Password"))
        {
            LockIcon.gameObject.SetActive(true);
        }
        else
        {
            LockIcon.gameObject.SetActive(false);
        }
    }




    public void Onclick()
    {
        LobbyManager.Instance.JoinRoom(Roominformation);
    }
}
