using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Pool : MonoBehaviour
{
    [Header("Instance")]
    public static Skill_Pool Instance;

    [Header("Handle Amount Skill")]
    int AmountSkill = 10;

    [SerializeField] GameObject WaterBall;
    List<GameObject> ListWaterBall = new List<GameObject>();

    [SerializeField] GameObject WaterBallExplosion;
    List<GameObject> ListWaterBallExplosion = new List<GameObject>();

    [SerializeField] GameObject WaterSlash;
    List<GameObject> ListWaterSlash = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameObject obj;

        for (int i = 0; i < AmountSkill; i++)
        {
            obj = Instantiate(WaterBall);
            obj.SetActive(false);
            ListWaterBall.Add(obj);
        }

        for (int i = 0; i < AmountSkill; i++)
        {
            obj = Instantiate(WaterBallExplosion);
            obj.SetActive(false);
            ListWaterBallExplosion.Add(obj);
        }

        for (int i = 0; i < AmountSkill; i++)
        {
            obj = Instantiate(WaterSlash);
            obj.SetActive(false);
            ListWaterSlash.Add(obj);
        }
    }

    public GameObject GetWaterBallFromPool()
    {
        for (int i = 0; i < ListWaterBall.Count; i++)
        {
            if (!ListWaterBall[i].activeInHierarchy)
            {
                return ListWaterBall[i];
            }
        }
        return null;
    }

    public GameObject GetWaterBallExplosionFromPool()
    {
        for (int i = 0; i < ListWaterBallExplosion.Count; i++)
        {
            if (!ListWaterBallExplosion[i].activeInHierarchy)
            {
                return ListWaterBallExplosion[i];
            }
        }
        return null;
    }

    public GameObject GetWaterSlashFromPool()
    {
        for (int i = 0; i < ListWaterSlash.Count; i++)
        {
            if (!ListWaterSlash[i].activeInHierarchy)
            {
                return ListWaterSlash[i];
            }
        }
        return null;
    }
}
