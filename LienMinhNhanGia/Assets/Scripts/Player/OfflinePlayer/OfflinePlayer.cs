using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Burst.Intrinsics;
using UnityEngine;


public class OfflinePlayer : OfflineCharacter
{
    [Header("Instance")]
    public static OfflinePlayer Instance;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(DetectGroundTransform.position - DetectGroundTransform.up * DetectGroundDistance, DetectGroundVector);
        Gizmos.DrawWireSphere(AttackPoint.position , AttackRange);
    }
}
