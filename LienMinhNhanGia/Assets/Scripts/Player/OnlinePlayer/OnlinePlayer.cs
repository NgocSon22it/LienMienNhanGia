using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlinePlayer : OnlineCharacter
{
    [Header("Instance")]
    public static OnlinePlayer Instance;


    [SerializeField] TMP_Text PlayerNameTxt;
    [SerializeField] Vector3 Offset;
    

    private void Awake()
    {
        Instance = this;
    }
    new void Start()
    {
        PlayerNameTxt.text = photonView.Owner.NickName;
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
        PlayerNameTxt.transform.position = Camera.main.WorldToScreenPoint(transform.position + Offset);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(DetectGroundTransform.position - DetectGroundTransform.up * DetectGroundDistance, DetectGroundVector);
    }



    
}
