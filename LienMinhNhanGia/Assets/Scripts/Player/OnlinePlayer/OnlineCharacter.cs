using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using System;
using Unity.Burst.Intrinsics;

public class OnlineCharacter : MonoBehaviourPun, IPunObservable
{
    [Header("Common Value")]
    protected string Name;
    protected int TotalHealth;
    protected int CurrentHealth;
    protected int TotalChakra;
    protected int CurrentChakra;
    protected int MovementSpeed;

    [Header("Component")]
    public Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    public CapsuleCollider2D capsuleCollider2D;
    protected PhotonView PV;
    public AudioSource audioSource;

    [Header("Enviroment Interaction")]
    [SerializeField] protected LayerMask JumpAbleLayer;
    [SerializeField] protected LayerMask NotFallingLayer;
    [SerializeField] protected Vector2 DetectGroundVector;
    [SerializeField] protected Transform DetectGroundTransform;
    [SerializeField] protected float DetectGroundDistance;

    [Header("On hit")]
    protected int Strong, Frequency;
    protected float Duration;

    [Header("Skill Interaction")]
    [SerializeField] public Transform Skill_WaterBall_Transform;
    [SerializeField] public Transform Skill_WaterSlash_Transform;

    [Header("Change Value For Level Up")]
    protected int JumpPower;
    protected int JumpTime, JumpTimeMax = 2;
    protected bool IsFalling, IsGround, IsTouchSlope;
    protected float VelocityY;
    protected bool IsWalking;
    protected bool CanWalking;

    [Header("Hard Value")]
    float XInput, YInput;
    protected int Combo;
    protected bool CanCombo, IsFacingRight = true;

    [SerializeField] public Transform AttackPoint;
    [SerializeField] LayerMask LayerToAttack;
    [SerializeField] public float AttackRange;

    [Header("Online Show")]
    [SerializeField] TMP_Text PlayerNameUITxt;
    [SerializeField] Vector3 Offset;
    [SerializeField] GameObject PlayerHealthUI;
    [SerializeField] GameObject PlayerChakraUI;
    [SerializeField] Image PlayerCurrentHealthUI;
    [SerializeField] Image PlayerCurrentChakraUI;


    [Header("Audio")]
    [SerializeField] AudioClip AttackSound;
    [SerializeField] AudioClip WaterBallSound;
    [SerializeField] AudioClip WaterSlashSound;

    Vector3 realPosition;
    Quaternion realRotation;
    float currentTime = 0;
    double currentPacketTime = 0;
    double lastPacketTime = 0;
    Vector3 positionAtLastPacket = Vector3.zero;
    Quaternion rotationAtLastPacket = Quaternion.identity;

    bool IsHurt;
    public bool IsDie;

    public void Start()
    {
        SetUpSkill();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        PV = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
        PlayerNameUITxt.text = PV.Owner.NickName;
        SetUpPlayer();

        PlayerCurrentHealthUI.fillAmount = 1f;
        PlayerCurrentChakraUI.fillAmount = 1f;
        InvokeRepeating(nameof(CallRegen), 1f, 10f);
    }

    public void CallRegen()
    {
        PV.RPC(nameof(RegenChakra), RpcTarget.All);
    }

    [PunRPC]
    public void RegenChakra()
    {
        if (IsDie) return;
        if (CurrentChakra >= TotalChakra)
        {
            CurrentChakra = TotalChakra;
        }
        else
        {
            CurrentChakra += 1;
        }
        SetUpChakraUI();
    }
    public void Update()
    {
        if (IsDie) return;

        if (CanWalking)
        {
            XInput = Input.GetAxis("Horizontal");
            IsWalking = Mathf.Abs(XInput) > 0;
        }
        else
        {
            XInput = 0f;
            IsWalking = false;
        }

        PlayerNameUITxt.transform.position = Camera.main.WorldToScreenPoint(transform.position + Offset);
        PlayerHealthUI.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 6, 0));
        PlayerChakraUI.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 5.6f, 0));
        VelocityY = rigidbody2d.velocity.y;
        IsGround = CheckIsGround();
        IsTouchSlope = CheckIsTouchSlope();

        if (IsGround)
        {
            JumpTime = 1;
        }

        if (PV.IsMine)
        {
            Jump();
            NormalAttack();
            ExecuteSkill();
        }

        animator.SetBool("IsGround", IsGround);
        animator.SetBool("TouchSlope", IsTouchSlope);
        animator.SetBool("Falling", VelocityY < 0);
        animator.SetBool("FallingFromHighPlace", VelocityY < -10);
    }

    public void FixedUpdate()
    {
        if (IsDie) return;
        if (PV.IsMine)
        {
            Walk();

        }
        else
        {
            rigidbody2d.isKinematic = true;

            double timeToReachGoal = currentPacketTime - lastPacketTime;
            currentTime += Time.deltaTime;

            //Update remote player
            transform.position = Vector3.Lerp(positionAtLastPacket, realPosition, (float)(currentTime / timeToReachGoal));
            transform.rotation = Quaternion.Lerp(rotationAtLastPacket, realRotation, (float)(currentTime / timeToReachGoal));

        }

    }

    public void PlaySoundAttack()
    {
        audioSource.clip = AttackSound;
        audioSource.Play();
    }

    public void PlaySoundWaterBall()
    {
        audioSource.clip = WaterBallSound;
        audioSource.Play();
    }

    public void PlaySoundWaterSlash()
    {
        audioSource.clip = WaterSlashSound;
        audioSource.Play();
    }
    // RPC to sync position and velocity across network

    public void SetUpPlayer()
    {
        SetUpHealth();
        SetUpChakra();
        PV.RPC(nameof(SetUpHealthUI), RpcTarget.All);
        PV.RPC(nameof(SetUpChakraUI), RpcTarget.All);
        SetUpSpeedAndJumpPower(27, 50);
    }
    public void NormalAttackDamage()
    {
        Collider2D[] HitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, LayerToAttack);

        if (HitEnemy != null)
        {
            foreach (Collider2D Enemy in HitEnemy)
            {
                if (Enemy.gameObject.CompareTag("Boss"))
                {
                    Enemy.GetComponent<Online_Shukaku>().TakeDamage(50);
                }
            }
        }
    }

    [PunRPC]
    public void SetUpHealthUI()
    {
        PlayerCurrentHealthUI.fillAmount = (float)GetCurrentHealth() / (float)GetTotalHealth();
    }
    [PunRPC]
    public void SetUpChakraUI()
    {
        PlayerCurrentChakraUI.fillAmount = (float)GetCurrentChakra() / (float)GetTotalChakra();
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


    public void TakeDamage(int damage)
    {
        if (IsHurt) return;
        CurrentHealth -= damage;
        PV.RPC(nameof(SetUpHealthUI), RpcTarget.All);
        CameraManager.Instance.StartShakeScreen(Strong, Frequency, Duration);
        StartCoroutine(DamageAnimation());
        if(CurrentHealth <= 0)
        {
            animator.SetTrigger("Die");
        }

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
    [PunRPC]
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

    public void JumpHandle(float jumpPower)
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpPower);
        PV.RPC(nameof(SetTriggerJump), RpcTarget.All);
        Finishcombo();
    }

    #region Animator RPC

    [PunRPC]
    public void SetTriggerJump()
    {
        animator.SetTrigger("Jump");
    }

    [PunRPC]
    public void SetTriggerAttack()
    {
        animator.SetTrigger("Attack" + Combo);
    }

    [PunRPC]
    public void SetTriggerSkill_WaterBall()
    {
        animator.SetTrigger("Skill_WaterBall");
    }

    [PunRPC]
    public void SetTriggerSkill_WaterSlash()
    {
        animator.SetTrigger("Skill_WaterSlash");
    }


    #endregion

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
            PV.RPC(nameof(SetTriggerAttack), RpcTarget.All);
        }
    }
    public void Startcombo()
    {
        CanCombo = false;
        if (Combo < 2)
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


    public void Spawn_WaterBall()
    {
        GameObject waterball = Skill_Pool.Instance.GetWaterBallFromPool();

        if (waterball != null)
        {
            waterball.transform.position = Skill_WaterBall_Transform.position;
            waterball.transform.rotation = Skill_WaterBall_Transform.rotation;
            waterball.SetActive(true);
        }
    }

    public void Spawn_WaterSlash()
    {
        GameObject waterSlash = Skill_Pool.Instance.GetWaterSlashFromPool();

        if (waterSlash != null)
        {
            waterSlash.transform.position = Skill_WaterSlash_Transform.position;
            waterSlash.transform.rotation = Skill_WaterSlash_Transform.rotation;
            waterSlash.SetActive(true);
        }
    }

    AccountSkillEntity AccountSkill_U;
    AccountSkillEntity AccountSkill_I;
    AccountSkillEntity AccountSkill_O;

    SkillEntity Skill;
    SkillEntity Skill_U;
    SkillEntity Skill_I;
    SkillEntity Skill_O;

    public void Skill_WaterBall()
    {
        PV.RPC(nameof(SetTriggerSkill_WaterBall), RpcTarget.AllBuffered);
    }
    public void Skill_WaterSlash()
    {
        PV.RPC(nameof(SetTriggerSkill_WaterSlash), RpcTarget.AllBuffered);
    }


    public void SetUpSkill()
    {
        AccountSkillEntity accountSkillEntityU = new Account_SkillDAO().GetAccountSkillbySlotIndex(AccountManager.AccountID, 1);
        AccountSkillEntity accountSkillEntityI = new Account_SkillDAO().GetAccountSkillbySlotIndex(AccountManager.AccountID, 2);
        AccountSkillEntity accountSkillEntityO = new Account_SkillDAO().GetAccountSkillbySlotIndex(AccountManager.AccountID, 3);


        AccountSkill_U = accountSkillEntityU;
        if (AccountSkill_U != null) { Skill_U = new SkillDAO().GetSkillbyID(AccountSkill_U.SkillID); }
        else { Skill_U = null; }


        AccountSkill_I = accountSkillEntityI;
        if (AccountSkill_I != null) { Skill_I = new SkillDAO().GetSkillbyID(AccountSkill_I.SkillID); }
        else { Skill_I = null; }

        AccountSkill_O = accountSkillEntityO;
        if (AccountSkill_O != null) { Skill_O = new SkillDAO().GetSkillbyID(AccountSkill_O.SkillID); }
        else { Skill_O = null; }

    }


    public void CallMethodFromHold(string MethodName)
    {
        Invoke(MethodName, 0f);
    }

    public void ControlSkill(KeyCode key, AccountSkillEntity accountSkillEntity, SkillEntity skillEntity)
    {
        if (Input.GetKeyDown(key) && accountSkillEntity != null && skillEntity != null && GetCurrentChakra() >= (skillEntity.Chakra - accountSkillEntity.CurrentLevel))
        {
            Skill = new SkillDAO().GetSkillbyID(accountSkillEntity.SkillID);
            CallMethodFromHold(Skill.SkillID);
            PV.RPC(nameof(SetCurrentChakra), RpcTarget.All, GetCurrentChakra() - (skillEntity.Chakra - accountSkillEntity.CurrentLevel));
            PV.RPC(nameof(SetUpChakraUI), RpcTarget.All);
        }
    }

    public void ExecuteSkill() 
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ControlSkill(KeyCode.U, AccountSkill_U, Skill_U);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ControlSkill(KeyCode.I, AccountSkill_I, Skill_I);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ControlSkill(KeyCode.O, AccountSkill_O, Skill_O);
        }
    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            stream.SendNext(IsFalling);
            stream.SendNext(CanWalking);
            stream.SendNext(IsWalking);
            stream.SendNext(IsGround);
            stream.SendNext(IsTouchSlope);
            stream.SendNext(VelocityY);
            stream.SendNext(CurrentHealth);
            stream.SendNext(CurrentChakra);
            stream.SendNext(Combo);
            stream.SendNext(CanCombo);
            stream.SendNext(IsHurt);
        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();


            IsFalling = (bool)stream.ReceiveNext();
            CanWalking = (bool)stream.ReceiveNext();
            IsWalking = (bool)stream.ReceiveNext();
            IsGround = (bool)stream.ReceiveNext();
            IsTouchSlope = (bool)stream.ReceiveNext();
            VelocityY = (float)stream.ReceiveNext();
            CurrentHealth = (int)stream.ReceiveNext();
            CurrentChakra = (int)stream.ReceiveNext();
            Combo = (int)stream.ReceiveNext();
            CanCombo = (bool)stream.ReceiveNext();
            IsHurt = (bool)stream.ReceiveNext();

            //Lag compensation
            currentTime = 0.0f;
            lastPacketTime = currentPacketTime;
            currentPacketTime = info.SentServerTime;
            positionAtLastPacket = transform.position;
            rotationAtLastPacket = transform.rotation;

        }
    }
}

