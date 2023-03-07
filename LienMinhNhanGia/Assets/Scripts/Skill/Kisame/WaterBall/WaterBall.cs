using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
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
        Invoke(nameof(TurnOff), 5f);
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
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<Monster>().TakeDamage(0);
            }
            Explosion = Skill_Pool.Instance.GetWaterBallExplosionFromPool();         
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
