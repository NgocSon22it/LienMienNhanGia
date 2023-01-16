using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Burst.Intrinsics;
using UnityEngine;


public class PlayerOffLine : MonoBehaviour
{
    [Header("Instance")]
    public static PlayerOffLine Instance;

    [Header("Component")]
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D boxCollider;

    [Header("Hard Value")]
    float XInput, YInput;
    int Combo;
    bool CanCombo, IsFacingRight = true;

    [Header("Change Value For Level Up")]
    private int MovementSpeed, JumpPower, JumpTime;
    private bool CanDoubleJump;

    [Header("Enviroment Interaction")]
    [SerializeField] LayerMask JumpAbleLayer;
    [SerializeField] Vector2 DetectGroundVector;
    [SerializeField] Transform DetectGroundTransform;
    [SerializeField] float DetectGroundDistance;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        SetJumpPower(60);
        SetMovementSpeed(27);
        JumpTime = CanDoubleJump == true ? 2 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        XInput = Input.GetAxis("Horizontal");

        if(IsGround() && rb.velocity.y <= 0)
        {
            JumpTime = CanDoubleJump == true ? 2 : 1;
        }

        Jump();
        NormalAttack();

    }

    private void FixedUpdate()
    {
        Walk();
    }

    public void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && JumpTime > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            animator.SetTrigger("Jump");
            JumpTime--;
            Finishcombo();
        }

        animator.SetBool("IsGround", IsGround());
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

    public bool IsGround()
    {
        return Physics2D.BoxCast(DetectGroundTransform.position, DetectGroundVector, 0, -DetectGroundTransform.up, DetectGroundDistance, JumpAbleLayer);     
    }

    #region MovementSpeed
    public void SetMovementSpeed(int Speed)
    {
        MovementSpeed = Speed;
    }

    public int GetMovementSpeed()
    {
        return MovementSpeed;
    }

    #endregion

    #region JumpPower
    public void SetJumpPower(int JumpPower)
    {
        this.JumpPower = JumpPower;
    }

    public int GetJumpPower()
    {
        return JumpPower;
    }
    #endregion

    #region DoubleJump
    public void SetDoubleJump(bool CanDoubleJump)
    {
        this.CanDoubleJump = CanDoubleJump;
    }

    public bool GetCanDoubleJump()
    {
        return CanDoubleJump;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(DetectGroundTransform.position - DetectGroundTransform.up * DetectGroundDistance, DetectGroundVector);
    }
}
