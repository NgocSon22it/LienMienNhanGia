using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] int CheckPointID;

    public int GetCheckPointID()
    {
        return CheckPointID;
    }
}
