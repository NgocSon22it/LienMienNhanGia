using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{

    [SerializeField] TMP_Text PlayerName;

    Player player;

    public void SetUp(Player _player)
    {
        player = _player;
        PlayerName.text = player.NickName;
    }

/*    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }*/
}
