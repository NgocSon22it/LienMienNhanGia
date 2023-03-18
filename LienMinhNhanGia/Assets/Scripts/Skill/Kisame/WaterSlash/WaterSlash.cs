using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlash : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    [SerializeField] List<string> ListTag = new List<string>();
    [SerializeField] int Speed;
    GameObject Explosion;
    private void OnEnable()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = transform.right * Speed;
        Invoke(nameof(TurnOff), 2.5f);
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
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Slope"))
            {
                TurnOff();
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<Monster>().TakeDamage(0);
            }
            if (collision.gameObject.CompareTag("Boss"))
            {
                collision.GetComponent<Shukaku>().TakeDamage(100);
            }

            Explosion = Skill_Pool.Instance.GetWaterSlashExplosionFromPool();

            if (Explosion != null)
            {
                Explosion.transform.position = transform.position;
                Explosion.transform.rotation = transform.rotation;
                Explosion.SetActive(true);
            }
        }
    }
}
