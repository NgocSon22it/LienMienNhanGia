using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlinePlayer : OnlineCharacter
{
    public static OnlinePlayer LocalPlayerInstance
    {
        get
        {
            if (PhotonNetwork.LocalPlayer == null) return null;

            foreach (PhotonView photonView in PhotonView.FindObjectsOfType<PhotonView>())
            {
                if (photonView.IsMine && photonView.gameObject.GetComponent<OnlinePlayer>() != null)
                {
                    return photonView.gameObject.GetComponent<OnlinePlayer>();
                }
            }

            return null;
        }
    }

    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
    new void FixedUpdate()
    {
        base.FixedUpdate();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(DetectGroundTransform.position - DetectGroundTransform.up * DetectGroundDistance, DetectGroundVector);
    }




}
