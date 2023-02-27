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
        GameObject waterball = Skill_Pool.Instance.GetWaterBallFromPool();

        if (waterball != null)
        {
            waterball.transform.position = OfflinePlayer.Instance.Place_ExecuteSkill.position;
            waterball.transform.rotation = OfflinePlayer.Instance.Place_ExecuteSkill.rotation;
            waterball.SetActive(true);
        }
    }

    public void WaterSword()
    {
        Debug.Log("WaterSword");
    }
    public void WaterDragon()
    {
        Debug.Log("WaterDragon");
    }

    public void WateShark()
    {
        Debug.Log("WateShark");
    }

    public void CallMethodFromHold(string MethodName)
    {
        Invoke(MethodName, 0f);
    }




}
