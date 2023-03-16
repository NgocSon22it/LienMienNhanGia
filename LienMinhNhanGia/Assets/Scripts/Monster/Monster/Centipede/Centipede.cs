using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : Monster
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] BoxCollider2D CheckTrigger;


    new void Start()
    {
        Monster_ID = "Monster_Centipede";
        FacingRight = true;
        base.Start();
    }

    public void TurnOnCol()
    {
        boxCollider.enabled = true;
        CheckTrigger.enabled = true;
        HealthBar.gameObject.SetActive(true);
    }

    public void TurnOffCol()
    {
        boxCollider.enabled = false;
        CheckTrigger.enabled = false;
        HealthBar.gameObject.SetActive(false);
    }


    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<OfflinePlayer>().TakeDamage(Damage, transform);
            Debug.Log("ok");
        }
    }
}
