using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooh : Monster
{
    BoxCollider2D boxCollider2D;

    new void Start()
    {
        Monster_ID = "Monster_Tete";
        boxCollider2D = GetComponent<BoxCollider2D>();
        base.Start();
    }

    public void TurnOnCol()
    {
        boxCollider2D.enabled = true;
        HealthBar.gameObject.SetActive(true);
    }

    public void TurnOffCol()
    {
        boxCollider2D.enabled = false;
        HealthBar.gameObject.SetActive(false);
    }


    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void handleRotation()
    {
        if (transform.position.x > offlinePlayer.transform.position.x && FacingRight)
        {
            Flip();
        }
        else if (transform.position.x < offlinePlayer.transform.position.x && !FacingRight)
        {
            Flip();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<OfflinePlayer>().TakeDamage(1, transform);
        }
    }
}
