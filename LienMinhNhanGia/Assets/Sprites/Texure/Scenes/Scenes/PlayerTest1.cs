using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    Rigidbody2D rb;


    float Xinput;
    public bool FacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Xinput = Input.GetAxis("Horizontal");
        Jump();
    }

    private void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {

        rb.velocity = new Vector2(Xinput * 27, rb.velocity.y);
        if (Xinput < -0.01 && FacingRight)
        {
            Flip();
        }
        else if (Xinput > 0.01 && !FacingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, 60);

        }
    }
    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }



}
