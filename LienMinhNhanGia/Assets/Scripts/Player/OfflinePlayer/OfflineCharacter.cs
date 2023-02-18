using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OfflineCharacter : MonoBehaviour
{
    #region Common Value
    [Header("Common Value")]
    protected string Name;
    protected int TotalHealth;
    protected int CurrentHealth;
    protected int TotalChakra;
    protected int CurrentChakra;
    protected int MovementSpeed;
    #endregion

    #region Component
    [Header("Component")]
    protected Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected BoxCollider2D boxCollider2d;
    #endregion

    #region Enviroment Interaction
    [Header("Enviroment Interaction")]
    [SerializeField] protected LayerMask JumpAbleLayer;
    [SerializeField] protected LayerMask NotFallingLayer;
    [SerializeField] protected Vector2 DetectGroundVector;
    [SerializeField] protected Transform DetectGroundTransform;
    [SerializeField] protected float DetectGroundDistance;
    #endregion

    #region Account Bonus

    #endregion

    #region ScreenShake Control
    [Header("On hit")]
    protected int Strong, Frequency;
    protected float Duration;
    #endregion

    #region Value Change
    [Header("Change Value For Level Up")]
    protected int JumpPower;
    protected int JumpTime, JumpTimeMax = 1;
    public bool IsFalling;


    #endregion

    #region Hard Value
    [Header("Hard Value")]
    float XInput, YInput;
    protected int Combo;
    protected bool CanCombo, IsFacingRight = true;

    #endregion


    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
        XInput = Input.GetAxis("Horizontal");


        if (IsGround())
        {
            JumpTime = 1;
        }

        Jump();
        NormalAttack();
        Walk();
        animator.SetBool("IsGround", IsGround());
        animator.SetBool("TouchSlope", IsTouchSlope());
        animator.SetBool("Falling", rigidbody2d.velocity.y < 0); 
        animator.SetBool("FallingFromHighPlace", rigidbody2d.velocity.y < -10);
    }

    #region Set Up Player

    public void SetUpPlayer()
    {
        SetUpHealth();
        SetUpChakra();
        SetUpSpeedAndJumpPower(27, 50);
    }
    public void SetUpHealth()
    {
        TotalHealth = 10;
        CurrentHealth = TotalHealth;
    }
    public void SetUpChakra()
    {
        TotalChakra = 10;
        CurrentChakra = TotalChakra;
    }
    #endregion
    public void SetUpSpeedAndJumpPower(int Speed, int Jump)
    {
        MovementSpeed = Speed;
        JumpPower = Jump;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        CurrentChakra -= damage;
        CameraManager.Instance.StartShakeScreen(Strong, Frequency, Duration);
        PlayerUIManager.Instance.SetUpPlayerUI();
    }

    #region Health
    public void SetCurrentHealth(int Health)
    {
        CurrentHealth = Health;
    }
    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public void SetTotalHealth(int Health)
    {
        TotalHealth = Health;
    }
    public int GetTotalHealth()
    {
        return TotalHealth;
    }
    #endregion

    #region Chakra
    public void SetCurrentChakra(int Chakra)
    {
        CurrentChakra = Chakra;
    }
    public int GetCurrentChakra()
    {
        return CurrentChakra;
    }

    public void SetTotalChakra(int Chakra)
    {
        TotalChakra = Chakra;
    }
    public int GetTotalChakra()
    {
        return TotalChakra;
    }
    #endregion

    #region Normal Control
    public void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGround())
            {
                JumpHandle(JumpPower);
            }
            else
            {

                if (JumpTime < JumpTimeMax && !IsFalling)
                {
                    JumpHandle(JumpPower * 0.7f);
                    JumpTime++;
                }

            }
        }  
    }

    public void JumpHandle(float jumpPower)
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpPower);
        animator.SetTrigger("Jump");
        Finishcombo();
    }

    public void Walk()
    {

        rigidbody2d.velocity = Vector2.Lerp(rigidbody2d.velocity, new Vector2(XInput * MovementSpeed, rigidbody2d.velocity.y), Time.deltaTime * 10f);

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

    public bool IsTouchSlope()
    {
        return Physics2D.BoxCast(DetectGroundTransform.position, DetectGroundVector, 0, -DetectGroundTransform.up, DetectGroundDistance, NotFallingLayer);
    }
    #endregion

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
    public void SetJumpTimeMax(int Amount)
    {
        this.JumpTimeMax = Amount;
    }
    public int GetJumpTimeMax()
    {
        return JumpTimeMax;
    }
    #endregion
    public void Fall()
    {
        IsFalling = true;
    }
    public void NotFall()
    {
        IsFalling = false;
    }

}
