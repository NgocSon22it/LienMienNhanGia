using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] TMP_Text RoomNameTxt;
    [SerializeField] TMP_Text NumberPlayerTxt;

    [SerializeField] GameObject LockIcon;



    RoomInfo Roominformation;

    public void SetUp(RoomInfo _Roominformation)
    {
        Roominformation = _Roominformation;
        if (Roominformation.CustomProperties.ContainsKey("Creator"))
        {
            RoomNameTxt.text = "Phòng " + Roominformation.Name + " của " + Roominformation.CustomProperties["Creator"].ToString();
        }
        else
        {
            RoomNameTxt.text = "Phòng " + Roominformation.Name + "0";
        }
        NumberPlayerTxt.text = Roominformation.PlayerCount + " / " + Roominformation.MaxPlayers;

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
