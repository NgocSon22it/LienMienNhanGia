using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Online_Shukaku_EarthRock : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    [SerializeField] List<string> ListTag = new List<string>();
    GameObject Explosion;
    int Damage;

    private void OnEnable()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Invoke(nameof(TurnOff), 3.3f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void TurnOff()
    {
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ListTag.Contains(collision.gameObject.tag))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<OnlinePlayer>().TakeDamage(1);
            }
        }
    }
}
