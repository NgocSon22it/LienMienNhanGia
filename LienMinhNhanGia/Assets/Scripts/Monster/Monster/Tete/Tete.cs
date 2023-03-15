using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tete : Monster
{
    [SerializeField] EdgeCollider2D edgeCollider;
    [SerializeField] EdgeCollider2D CheckTrigger;


    new void Start()
    {
        Monster_ID = "Monster_Tete";
        FacingRight = true;
        base.Start();             
    }

    public void TurnOnCol()
    {
        edgeCollider.enabled = true;
        CheckTrigger.enabled = true;
        HealthBar.gameObject.SetActive(true);
    }

    public void TurnOffCol()
    {
        edgeCollider.enabled = false;
        CheckTrigger.enabled = false;
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
