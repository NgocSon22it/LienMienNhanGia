using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] TMP_Text RoomNameTxt;
    [SerializeField] TMP_Text NumberPlayerTxt;

    RoomInfo Roominformation;

    public void SetUp(RoomInfo _Roominformation)
    {
        Roominformation = _Roominformation;
        RoomNameTxt.text = "Phòng" + Roominformation.Name;
        NumberPlayerTxt.text = Roominformation.PlayerCount + " / " + Roominformation.MaxPlayers;
    }

    public void Onclick()
    {
        LobbyManager.Instance.JoinRoom(Roominformation);
    }
}
