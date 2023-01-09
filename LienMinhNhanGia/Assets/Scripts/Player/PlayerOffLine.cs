using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Burst.Intrinsics;
using UnityEngine;


public class PlayerOffLine : MonoBehaviour
{

    [Header("Component")]
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;


    [Header("Common Value")]
    [SerializeField] int MovementSpeed, JumpPower;
    float XInput, YInput;
    int Combo, CanJump = 0;
    bool CanCombo, IsGround, IsFacingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        XInput = Input.GetAxis("Horizontal");
        Jump();
        NormalAttack();
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
            animator.SetTrigger("Jump");
            IsGround = false;           
            CanJump--;
            Finishcombo();
        }
        animator.SetBool("IsGround", IsGround);
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


    public void NormalAttack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !CanCombo)
        {
            CanCombo = true;
            animator.SetTrigger("Attack" + Combo);
        }
    }
    public void Startcombo()
    {
        CanCombo = false;
        if (Combo < 3)
        {
            Combo++;
        }
    }

    public void Finishcombo()
    {
        CanCombo = false;
        Combo = 0;
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
