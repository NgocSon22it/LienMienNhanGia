using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

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
    protected Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected BoxCollider2D boxCollider2d;
    protected PhotonView PV;

    [Header("Enviroment Interaction")]
    [SerializeField] protected LayerMask JumpAbleLayer;
    [SerializeField] protected LayerMask NotFallingLayer;
    [SerializeField] protected Vector2 DetectGroundVector;
    [SerializeField] protected Transform DetectGroundTransform;
    [SerializeField] protected float DetectGroundDistance;

    [Header("On hit")]
    protected int Strong, Frequency;
    protected float Duration;

    [Header("Change Value For Level Up")]
    protected int JumpPower;
    protected int JumpTime, JumpTimeMax = 1;
    protected bool IsFalling, IsGround, IsTouchSlope;
    protected float VelocityY;
    protected bool IsWalking;
    protected bool CanWalking;

    [Header("Hard Value")]
    float XInput, YInput;
    protected int Combo;
    protected bool CanCombo, IsFacingRight = true;

    [Header("Online Show")]
    [SerializeField] GameObject OnlineCamera;
    [SerializeField] GameObject PlayerUI;
    [SerializeField] TMP_Text PlayerNameTxt;
    [SerializeField] Vector3 Offset;

    Vector3 realPosition;
    Quaternion realRotation;
    float currentTime = 0;
    double currentPacketTime = 0;
    double lastPacketTime = 0;
    Vector3 positionAtLastPacket = Vector3.zero;
    Quaternion rotationAtLastPacket = Quaternion.identity;

    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        PV = GetComponent<PhotonView>();
        PlayerNameTxt.text = PV.Owner.NickName;
        SetUpPlayer();
        if (PV.IsMine)
        {
            GameObject OnlineCameraFollow = Instantiate(OnlineCamera);
            OnlineCameraFollow.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = Online_GameManager.Instance.GetSkyBoxCollider();
            OnlineCameraFollow.GetComponent<CinemachineVirtualCamera>().m_Follow = gameObject.transform;
        }
        else
        {

        }
    }

    public void Update()
    {
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
        PlayerNameTxt.transform.position = Camera.main.WorldToScreenPoint(transform.position + Offset);
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
            PV.RPC(nameof(NormalAttack), RpcTarget.AllBuffered);
            Walk();
        }

        animator.SetBool("IsGround", IsGround);
        animator.SetBool("TouchSlope", IsTouchSlope);
        animator.SetBool("Falling", VelocityY < 0);
        animator.SetBool("FallingFromHighPlace", VelocityY < -10);
    }

    public void FixedUpdate()
    {

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
    // RPC to sync position and velocity across network

    public void SetUpPlayer()
    {
        //SetUpHealth();
        //SetUpChakra();
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
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        CurrentChakra -= damage;
        CameraManager.Instance.StartShakeScreen(Strong, Frequency, Duration);
        PlayerUIManager.Instance.SetUpPlayerUI();
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

    public void JumpHandle(float jumpPower)
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpPower);
        PV.RPC(nameof(SetTriggerJump), RpcTarget.AllBuffered);
        Finishcombo();
    }

    #region Animator RPC

    [PunRPC]
    public void SetTriggerJump()
    {
        animator.SetTrigger("Jump");
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

    [PunRPC]
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            stream.SendNext(IsFalling);
            stream.SendNext(CanWalking);
            stream.SendNext(IsGround);
            stream.SendNext(IsTouchSlope);
            stream.SendNext(VelocityY);

        }
        else
        {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();


            IsFalling = (bool)stream.ReceiveNext();
            CanWalking = (bool)stream.ReceiveNext();
            IsGround = (bool)stream.ReceiveNext();
            IsTouchSlope = (bool)stream.ReceiveNext();
            VelocityY = (float)stream.ReceiveNext();

            //Lag compensation
            currentTime = 0.0f;
            lastPacketTime = currentPacketTime;
            currentPacketTime = info.SentServerTime;
            positionAtLastPacket = transform.position;
            rotationAtLastPacket = transform.rotation;

        }
    }
}

