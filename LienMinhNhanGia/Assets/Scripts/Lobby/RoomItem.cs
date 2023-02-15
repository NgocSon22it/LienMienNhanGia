using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{
    [SerializeField] TMP_Text RoomName;

    RoomInfo Roominformation;

    public void SetUp(RoomInfo _Roominformation)
    {
        Roominformation = _Roominformation;
        RoomName.text = "Phòng" + _Roominformation.Name + " " + _Roominformation.PlayerCount + " / " + _Roominformation.MaxPlayers;
    }

    public void Onclick()
    {
        LobbyManager.Instance.JoinRoom(Roominformation);
    }
}
