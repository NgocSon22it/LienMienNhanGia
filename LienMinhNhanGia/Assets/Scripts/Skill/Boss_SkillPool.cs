using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SkillPool : MonoBehaviour
{
    [Header("Handle Amount Skill")]
    [SerializeField]int AmountSkill;

    [SerializeField] GameObject GroundSlash;
    List<GameObject> ListGroundSlash = new List<GameObject>();

    [SerializeField] GameObject GroundSlashExplosion;
    List<GameObject> ListGroundSlashExplosion = new List<GameObject>();

    [SerializeField] GameObject BeastBomb;
    List<GameObject> ListBeastBomb = new List<GameObject>();

    [SerializeField] GameObject BeastBombExplosion;
    List<GameObject> ListBeastBombExplosion = new List<GameObject>();

    [SerializeField] GameObject FirstRock;
    List<GameObject> ListFirstRock = new List<GameObject>();

    [SerializeField] GameObject EarthRock;
    List<GameObject> ListEarthRock = new List<GameObject>();

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

        for (int i = 0; i < AmountSkill; i++)
        {
            obj = Instantiate(BeastBomb);
            obj.SetActive(false);
            ListBeastBomb.Add(obj);
        }

        for (int i = 0; i < AmountSkill; i++)
        {
            obj = Instantiate(BeastBombExplosion);
            obj.SetActive(false);
            ListBeastBombExplosion.Add(obj);
        }
        for (int i = 0; i < AmountSkill; i++)
        {
            obj = Instantiate(FirstRock);
            obj.SetActive(false);
            ListFirstRock.Add(obj);
        }
        for (int i = 0; i < AmountSkill; i++)
        {
            obj = Instantiate(EarthRock);
            obj.SetActive(false);
            ListEarthRock.Add(obj);
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

    public GameObject GetBeastBombFromPool()
    {
        for (int i = 0; i < ListBeastBomb.Count; i++)
        {
            if (!ListBeastBomb[i].activeInHierarchy)
            {
                return ListBeastBomb[i];
            }
        }
        return null;
    }
    public GameObject GetBeastBombExplosionFromPool()
    {
        for (int i = 0; i < ListBeastBombExplosion.Count; i++)
        {
            if (!ListBeastBombExplosion[i].activeInHierarchy)
            {
                return ListBeastBombExplosion[i];
            }
        }
        return null;
    }

    public GameObject GetFirstRockFromPool()
    {
        for (int i = 0; i < ListFirstRock.Count; i++)
        {
            if (!ListFirstRock[i].activeInHierarchy)
            {
                return ListFirstRock[i];
            }
        }
        return null;
    }
    public GameObject GetEarthRockFromPool()
    {
        for (int i = 0; i < ListEarthRock.Count; i++)
        {
            if (!ListEarthRock[i].activeInHierarchy)
            {
                return ListEarthRock[i];
            }
        }
        return null;
    }
}
