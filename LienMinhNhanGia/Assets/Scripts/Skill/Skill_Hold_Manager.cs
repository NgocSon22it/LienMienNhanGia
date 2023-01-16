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

    public void Skill1()
    {
        Debug.Log("Skill1");
    }

    public void Skill2()
    {
        Debug.Log("Skill2");
    }
    public void Skill3()
    {
        Debug.Log("Skill3");
    }

    public void CallMethodFromHold(string MethodName)
    {
        Invoke(MethodName, 0f);
    }
}
