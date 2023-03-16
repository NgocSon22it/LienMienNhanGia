using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Black_Dog : Monster
{
    CapsuleCollider2D capsuleCollider2D;

    [Header("Patrol Points")]
    [SerializeField] private Transform PointA;
    [SerializeField] private Transform PointB;
    private Vector2 currentTarget;
    float direction;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Collider Area")]
    [SerializeField] private float AreaDistance;
    [SerializeField] private BoxCollider2D AreaCollider;
    [SerializeField] Transform AreaTransform;


    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] GameObject Bite;
    [SerializeField] Transform PlaceBite;

    bool CanMove;

    new void Start()
    {
        Monster_ID = "Monster_Blackdog";
        FacingRight = false;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        base.Start();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (PlayerEnterArea())
        {
            if (PlayerInSight())
            {
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    animator.SetTrigger("Attack");
                    idleDuration = 0;
                }
            }
            else
            {
                idleDuration += Time.fixedDeltaTime;
                if (idleDuration >= 1f)
                {
                    direction = offlinePlayer.transform.position.x - transform.position.x;

                    currentTarget = new Vector2(offlinePlayer.transform.position.x, transform.position.y);

                    transform.position = Vector2.MoveTowards(transform.position, currentTarget, Speed * Time.fixedDeltaTime);
                    animator.SetBool("Walk", true);
                    if ((direction > 0 && !FacingRight) || (direction < 0 && FacingRight))
                    {
                        Flip();
                    }
                }

            }
        }
        else
        {
            animator.SetBool("Walk", false);
        }

    }


    public void TurnOnCol()
    {
        capsuleCollider2D.enabled = true;
        HealthBar.gameObject.SetActive(true);
    }

    public void TurnOffCol()
    {
        capsuleCollider2D.enabled = false;
        HealthBar.gameObject.SetActive(false);
    }


    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    public void AttackDamage()
    {
        GameObject obj = Instantiate(Bite, PlaceBite.position, Quaternion.identity);
        Destroy(obj, 2f);
        StartCoroutine(DamagePlayer());
    }
    private bool PlayerEnterArea()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(AreaCollider.bounds.center,
            new Vector3(AreaCollider.bounds.size.x, AreaCollider.bounds.size.y, AreaCollider.bounds.size.z),
            0, Vector2.zero, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        Gizmos.DrawWireCube(AreaCollider.bounds.center,
            new Vector3(AreaCollider.bounds.size.x, AreaCollider.bounds.size.y, AreaCollider.bounds.size.z));
    }

    IEnumerator DamagePlayer()
    {
        yield return new WaitForSeconds(.2f);
        if (PlayerInSight())
        {
            offlinePlayer.GetComponent<OfflinePlayer>().TakeDamage(Damage, transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<OfflinePlayer>().TakeDamage(Damage, transform);
        }
    }

}
