using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlinePlayer : OnlineCharacter
{
    [Header("Instance")]
    public static OnlinePlayer Instance;

    private void Awake()
    {
        Instance = this;
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
