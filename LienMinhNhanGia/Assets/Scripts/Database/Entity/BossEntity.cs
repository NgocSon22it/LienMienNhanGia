using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntity
{
    public string BossID;
    public string BossName;

    public BossEntity(string BossID, string BossName)
    {
        this.BossID = BossID; this.BossName = BossName;
    }
}
