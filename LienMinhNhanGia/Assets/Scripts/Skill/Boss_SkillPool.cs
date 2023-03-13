using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SkillPool : MonoBehaviour
{
    [Header("Handle Amount Skill")]
    int AmountSkill = 5;

    [SerializeField] GameObject GroundSlash;
    List<GameObject> ListGroundSlash = new List<GameObject>();

    [SerializeField] GameObject GroundSlashExplosion;
    List<GameObject> ListGroundSlashExplosion = new List<GameObject>();

    public static Boss_SkillPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameObject obj;

        for (int i = 0; i < AmountSkill; i++)
        {
            obj = Instantiate(GroundSlash);
            obj.SetActive(false);
            ListGroundSlash.Add(obj);
        }

        for (int i = 0; i < AmountSkill; i++)
        {
            obj = Instantiate(GroundSlashExplosion);
            obj.SetActive(false);
            ListGroundSlashExplosion.Add(obj);
        }
    }

    public GameObject GetGroundSlashFromPool()
    {
        for (int i = 0; i < ListGroundSlash.Count; i++)
        {
            if (!ListGroundSlash[i].activeInHierarchy)
            {
                return ListGroundSlash[i];
            }
        }
        return null;
    }
    public GameObject GetGroundSlashExplosionFromPool()
    {
        for (int i = 0; i < ListGroundSlashExplosion.Count; i++)
        {
            if (!ListGroundSlashExplosion[i].activeInHierarchy)
            {
                return ListGroundSlashExplosion[i];
            }
        }
        return null;
    }
}
