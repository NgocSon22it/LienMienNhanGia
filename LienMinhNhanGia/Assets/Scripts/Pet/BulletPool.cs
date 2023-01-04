using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    List<GameObject> ListBullet = new List<GameObject>();
    public int AmountBullet = 20;


    [SerializeField] GameObject Bullet;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameObject obj;

        for(int i = 0; i < AmountBullet; i++)
        {
            obj = Instantiate(Bullet);
            obj.SetActive(false);
            ListBullet.Add(obj);
        }
    }

    public GameObject GetBulletFromPool()
    {
        for(int i = 0; i < ListBullet.Count; i++)
        {
            if (!ListBullet[i].activeInHierarchy)
            {
                return ListBullet[i];
            }
        }
        return null;
    }
}
