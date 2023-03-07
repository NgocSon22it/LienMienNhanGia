using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OfflineCharacter : MonoBehaviour
{
    [Header("Common Value")]
    protected string Name;
    protected int TotalHealth;
    protected int CurrentHealth;
    protected int TotalChakra;
    protected int CurrentChakra;
    protected int MovementSpeed;

    [Header("Component")]
    protected Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Animator animator;
    protected BoxCollider2D boxCollider2d;

    [Header("Enviroment Interaction")]
    [SerializeField] protected LayerMask JumpAbleLayer;
    [SerializeField] protected LayerMask NotFallingLayer;
    [SerializeField] protected Vector2 DetectGroundVector;
    [SerializeField] protected Transform DetectGroundTransform;
    [SerializeField] protected float DetectGroundDistance;

    [Header("Skill Interaction")]
    [SerializeField] public Transform Skill_WaterBall_Transform;
    [SerializeField] public Transform Skill_WaterSlash_Transform;


    [SerializeField] public Transform AttackPoint;
    [SerializeField] LayerMask LayerToAttack;
    [SerializeField] public float AttackRange;

    [Header("On hit")]
    public int Strong, Frequency;
    public float Duration;
    bool IsHurt;

    [Header("Change Value For Level Up")]
    protected int JumpPower;
    protected int JumpTime, JumpTimeMax = 1;
    protected bool IsFalling;
    protected bool IsWalking;
    protected bool CanWalking;
    protected bool IsGround, IsTouchSlope;
    protected float VelocityY;

    [Header("Hard Value")]
    float XInput, YInput;
    protected int Combo;
    protected bool CanCombo, IsFacingRight = true;

    [Header("Knockback")]
    public float KnockBackForce = 10f;
    public float KnockBackForceUp = 2f;
    bool IsKnockBack;



    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        SetUpPlayer();
        InvokeRepeating(nameof(RegenChakra), 1f, 10f);
    }

    public void Update()
    {
        if (CanWalking && !IsKnockBack)
        {
            XInput = Input.GetAxis("Horizontal");
            IsWalking = Mathf.Abs(XInput) > 0;
        }
        else
        {
            XInput = 0f;
            IsWalking = false;
        }

        VelocityY = rigidbody2d.velocity.y;
        IsGround = CheckIsGround();
        IsTouchSlope = CheckIsTouchSlope();

        if (IsGround)
        {
            JumpTime = 1;
        }

        Jump();
        NormalAttack();
        Walk();

        animator.SetBool("IsGround", IsGround);
        animator.SetBool("TouchSlope", IsTouchSlope);
        animator.SetBool("Falling", VelocityY < 0);
        animator.SetBool("FallingFromHighPlace", VelocityY < -10);
    }

    public void RegenChakra()
    {
        if (CurrentChakra >= TotalChakra)
        {
            CurrentChakra = TotalChakra;
        }
        else
        {
            CurrentChakra += 1;
        }
        PlayerUIManager.Instance.UpdatePlayerChakraUI();
    }

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
    public void SetUpSpeedAndJumpPower(int Speed, int Jump)
    {
        MovementSpeed = Speed;
        JumpPower = Jump;
    }
    public void TakeDamage(int damage, Transform transform)
    {
        if (IsHurt) { return; }
        CurrentHealth -= damage;
        CameraManager.Instance.StartShakeScreen(Strong, Frequency, Duration);
        PlayerUIManager.Instance.UpdatePlayerHealthUI();
        KnockBack(transform);
        StartCoroutine(DamageAnimation());
    }
    public IEnumerator DamageAnimation()
    {
        IsHurt = true;
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(.1f);

            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
        IsHurt = false;
    }
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

    public void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGround)
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

    public void NormalAttackDamage()
    {
        Collider2D[] HitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, LayerToAttack);

        if (HitEnemy != null)
        {
            foreach (Collider2D Enemy in HitEnemy)
            {
                if (Enemy.gameObject.CompareTag("Enemy"))
                {
                    Enemy.GetComponent<Monster>().TakeDamage(0);
                }
                if (Enemy.gameObject.CompareTag("BreakItem"))
                {
                    Enemy.GetComponent<BreakItem>().Break();
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
        animator.SetBool("Walking", IsWalking);
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
    public bool CheckIsGround()
    {
        return Physics2D.BoxCast(DetectGroundTransform.position, DetectGroundVector, 0, -DetectGroundTransform.up, DetectGroundDistance, JumpAbleLayer);
    }

    public bool CheckIsTouchSlope()
    {
        return Physics2D.BoxCast(DetectGroundTransform.position, DetectGroundVector, 0, -DetectGroundTransform.up, DetectGroundDistance, NotFallingLayer);
    }

    public void SetMovementSpeed(int Speed)
    {
        MovementSpeed = Speed;
    }

    public int GetMovementSpeed()
    {
        return MovementSpeed;
    }


    public void SetJumpPower(int JumpPower)
    {
        this.JumpPower = JumpPower;
    }

    public int GetJumpPower()
    {
        return JumpPower;
    }

    public void SetJumpTimeMax(int Amount)
    {
        this.JumpTimeMax = Amount;
    }
    public int GetJumpTimeMax()
    {
        return JumpTimeMax;
    }


    public void Amation_SetUpFall(bool value)
    {
        IsFalling = value;
    }

    public void Amation_SetUpWalk(bool value)
    {
        CanWalking = value;
    }

    public void KnockBack(Transform t)
    {
        Vector2 direction = new Vector2(transform.position.x - t.position.x, 0);
        rigidbody2d.velocity = new Vector2(direction.x, KnockBackForceUp) * KnockBackForce;
        StartCoroutine(Knockback());
    }

    public void Skill_WaterBall()
    {
        GameObject waterball = Skill_Pool.Instance.GetWaterBallFromPool();

        if (waterball != null)
        {
            waterball.transform.position = Skill_WaterBall_Transform.position;
            waterball.transform.rotation = Skill_WaterBall_Transform.rotation;
            waterball.SetActive(true);
        }
    }
    public void Skill_WaterSlash()
    {
        GameObject waterslash = Skill_Pool.Instance.GetWaterSlashFromPool();

        if (waterslash != null)
        {
            waterslash.transform.position = Skill_WaterSlash_Transform.position;
            waterslash.transform.rotation = Skill_WaterSlash_Transform.rotation;
            waterslash.SetActive(true);
        }
    }

    public IEnumerator Knockback()
    {
        IsKnockBack = true;
        yield return new WaitForSeconds(.5f);
        IsKnockBack = false;
    }
}
