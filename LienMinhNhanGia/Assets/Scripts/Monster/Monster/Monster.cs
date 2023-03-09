using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Monster : MonoBehaviour
{
    protected string Monster_ID;
    protected string Name;
    protected int Health;
    protected int Damage;
    protected int Speed;
    protected int Coin_Bonus;
    protected string Description;
    protected string Link_image;
    protected bool Delete;

    // Handle
    protected GameObject offlinePlayer;
    protected int CurrentHealth;
    [SerializeField] protected MonsterUI HealthBar;


    [Header("Component")]
    protected Animator animator;
    protected Rigidbody2D rigidbody2d;


    protected bool FacingRight;

    public void Start()
    {
        offlinePlayer = GameObject.FindGameObjectWithTag("Player");
        SetUpMonster();
        SetUpHealthBar();
        SetUpComponent();
    }

    public void SetUpComponent()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void SetUpMonster()
    {
        MonsterEntity monsterEntity = new MonsterDAO().GetMonsterbyId(Monster_ID);
        if (monsterEntity != null)
        {
            Name = monsterEntity.Name;
            Health = monsterEntity.Health;
            CurrentHealth = Health;
            Damage = monsterEntity.Damage;
            Speed = monsterEntity.Speed;
            Coin_Bonus = monsterEntity.Coin_Bonus;
            Description = monsterEntity.Description;
            Link_image = monsterEntity.Link_image;
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(DamageAnimation());
        SetUpHealthBar();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void DeactiveMonster()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color = Color.red;
        yield return new WaitForSeconds(.2f);
        sp.color = Color.white;
    }

    public void SetUpHealthBar()
    {
        HealthBar.SetHealth(CurrentHealth, Health);
    }

    // find closest Player to hit
    public GameObject FindClostestPlayer(int Range)
    {
        float distanceToClosestPlayer = Mathf.Infinity;
        GameObject closestPlayer = null;
        GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject currentPlayer in allPlayer)
        {
            float distanceToEnemy = (currentPlayer.transform.Find("MainPoint").position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestPlayer && Vector2.Distance(currentPlayer.transform.Find("MainPoint").position, transform.position) <= Range)
            {
                distanceToClosestPlayer = distanceToEnemy;
                closestPlayer = currentPlayer;
            }
        }
        return closestPlayer;
    }
}
