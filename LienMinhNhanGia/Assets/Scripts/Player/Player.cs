using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Burst.Intrinsics;
using UnityEngine;


public class Player : MonoBehaviour
{

    [Header("Component")]
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [Header("Common Value")]
    float XInput, YInput;
    bool IsFacingRight = true;
    [SerializeField] int MovementSpeed, JumpPower;
    int CanJump = 0;
    bool IsGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        XInput = Input.GetAxis("Horizontal");
        Jump();
    }

    private void FixedUpdate()
    {
        
        Walk();
        
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            IsGround = false;
            CanJump--;
        }
    }

    public void Walk()
    {
        rb.velocity = new Vector2(XInput * MovementSpeed, rb.velocity.y);
        if (XInput < -0.01 && IsFacingRight)
        {
            Flip();
        }
        else if (XInput > 0.01 && !IsFacingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGround = true;
            CanJump = 2;
        }

    }
}
