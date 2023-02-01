using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFall : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            rb.gravityScale = 15f;
        }
    }
}
