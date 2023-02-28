using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakItemPool : MonoBehaviour
{

    [Header("Instance")]
    public static BreakItemPool Instance;

    [Header("Handle Amount Skill")]
    int Amount = 20;

    [SerializeField] GameObject Rock_Explosion;
    List<GameObject> ListRock_Explosion = new List<GameObject>();

    [SerializeField] GameObject Leaf_Explosion;
    List<GameObject> ListLeaf_Explosion = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj;

        for (int i = 0; i < Amount; i++)
        {
            obj = Instantiate(Rock_Explosion);
            obj.SetActive(false);
            ListRock_Explosion.Add(obj);
        }

        for (int i = 0; i < Amount; i++)
        {
            obj = Instantiate(Leaf_Explosion);
            obj.SetActive(false);
            ListLeaf_Explosion.Add(obj);
        }
    }

    public GameObject GetListRock_ExplosionFromPool()
    {
        for (int i = 0; i < ListRock_Explosion.Count; i++)
        {
            if (!ListRock_Explosion[i].activeInHierarchy)
            {
                return ListRock_Explosion[i];
            }
        }
        return null;
    }

    public GameObject GetListLeaf_ExplosionFromPool()
    {
        for (int i = 0; i < ListLeaf_Explosion.Count; i++)
        {
            if (!ListLeaf_Explosion[i].activeInHierarchy)
            {
                return ListLeaf_Explosion[i];
            }
        }
        return null;
    }

}
