using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tete : Monster
{

    EdgeCollider2D edgeCollider2D;

     void Start()
    {
        Monster_ID = "Monster_Tete";
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        SetUpMonster();
    }

    public void TurnOnCol()
    {
        edgeCollider2D.enabled = true;
    }

    public void TurnOffCol()
    {
        edgeCollider2D.enabled = false;
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
