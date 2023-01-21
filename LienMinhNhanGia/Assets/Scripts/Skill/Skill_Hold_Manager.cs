using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Hold_Manager : MonoBehaviour
{
    public static Skill_Hold_Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void WaterBall()
    {
        Debug.Log("WaterBall");
    }

    public void WaterSword()
    {
        Debug.Log("WaterSword");
    }
    public void WaterDragon()
    {
        Debug.Log("WaterDragon");
    }

    public void CallMethodFromHold(string MethodName)
    {
        Invoke(MethodName, 0f);
    }
}
