using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlash : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    [SerializeField] List<string> ListTag = new List<string>();
    [SerializeField] int Speed;
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
            Debug.Log("Ok");
        }
    }
}
