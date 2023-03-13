using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shukaku_GroundSlash : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    [SerializeField] List<string> ListTag = new List<string>();
    [SerializeField] int Speed;
    GameObject Explosion;
    int Damage;

    private void OnEnable()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = transform.right * Speed;
        Invoke(nameof(TurnOff), 10f);
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
                collision.GetComponent<OfflineCharacter>().TakeDamage(1, transform);
            }
            else
            {
                Explosion = Boss_SkillPool.Instance.GetGroundSlashExplosionFromPool();
                if (Explosion != null)
                {
                    Explosion.transform.position = transform.position;
                    Explosion.transform.rotation = transform.rotation;
                    Explosion.SetActive(true);
                }
                TurnOff();
            }
        }
    }

}
