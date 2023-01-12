using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("Instance")]
    public static BulletPool Instance;

    [Header("Handle Bullet")]
    int AmountBullet = 20;
    [SerializeField] GameObject Bullet;
    List<GameObject> ListBullet = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
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
