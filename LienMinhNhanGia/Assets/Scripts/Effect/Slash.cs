using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public int Speed;
    Rigidbody2D rb;
    private void OnEnable()
    {
        rb= GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*Speed;
        Invoke("TurnOff", 0.3f);
    }


    private void OnDisable()
    {
        CancelInvoke();
    }

    void TurnOff()
    {
        gameObject.SetActive(false);
    }


}
