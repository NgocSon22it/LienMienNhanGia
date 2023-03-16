using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Shukaku : MonoBehaviour
{
    public string BossID;
    public string Name;
    public int Health;
    public int Speed;
    public int Coin_Bonus;
    public int Experience_Bonus;
    public int Point_Skill;

    public int CurrentHealth;

    GameObject obj;

    [SerializeField] BossHealthUI BossHealth;

    CircleCollider2D circleCollider2D;
    Animator animator;
    SpriteRenderer sp;
    GameObject Player;
    private Quaternion Rotation;
    public bool IsDead;

    [SerializeField] Transform Transform_GroundSlash;
    [SerializeField] Transform Transform_BeastBomb;
    [SerializeField] Transform Transform_EarthRock;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
        obj = Boss_SkillPool.Instance.GetBeastBombFromPool();
        StartCoroutine(StartGame());
    }



    public void SetUpBossFight()
    {
        BossEntity bossEntity = new BossDAO().GetBossByID("Boss_Shukaku");
        BossID = bossEntity.BossID;
        Name = bossEntity.Name;
        Health = bossEntity.Health;
        CurrentHealth = Health;
        Speed = bossEntity.Speed;
        Coin_Bonus = bossEntity.Coin_Bonus;
        Experience_Bonus = bossEntity.Experience_Bonus;
        Point_Skill = bossEntity.Point_Skill;
        BossHealth.SetUpHealth();       
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(DamageAnimation());
        BossHealth.SetUpHealth();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        GameManager.Instance.NormalCamera();
        IsDead = true;
        obj.SetActive(false);
        StopAllCoroutines();
        gameObject.SetActive(false);
        LevelManager.Instance.AddExperience(Experience_Bonus);

    }
    IEnumerator DamageAnimation()
    {

        sp.color = Color.red;
        yield return new WaitForSeconds(.2f);
        sp.color = Color.white;
    }

    public void FirstSkill()
    {
        StartCoroutine(ExecuteFirstSkill());
    }
    public void SecondSkill()
    {
        StartCoroutine(ExecuteThirdSkill());
    }
    public void ThirdSkill()
    {
        StartCoroutine(ExecuteThirdSkill());
    }
    public void FouthSkill()
    {
        StartCoroutine(ExecuteFouthSkill());
    }
    IEnumerator ExecuteFirstSkill()
    {
        obj = Boss_SkillPool.Instance.GetGroundSlashFromPool();

        if (obj != null)
        {
            obj.transform.position = Transform_GroundSlash.position;
            obj.transform.rotation = Transform_GroundSlash.rotation;
            obj.SetActive(true);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(Move());
    }
    IEnumerator ExecuteThirdSkill()
    {

        yield return new WaitForSeconds(1f);
        obj = Boss_SkillPool.Instance.GetBeastBombFromPool();
        if (obj != null)
        {
            obj.transform.position = Transform_BeastBomb.position;
            obj.transform.rotation = Transform_BeastBomb.rotation;
            obj.SetActive(true);
        }
        yield return new WaitForSeconds(4f);
        Vector2 direction = (Vector2)Player.transform.Find("MainPoint").position - (Vector2)Transform_BeastBomb.position;
        direction.Normalize();
        obj.GetComponent<Rigidbody2D>().AddForce(direction * 3000);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("ThirdSkill", false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Move());

    }
    IEnumerator ExecuteFouthSkill()
    {
        yield return new WaitForSeconds(1f);
        Transform_EarthRock.position = new Vector3(Player.transform.position.x, 0, 3);
        Vector3 localPos = Transform_EarthRock.position;
        localPos.x = Player.transform.position.x;

        obj = Boss_SkillPool.Instance.GetFirstRockFromPool();
        if (obj != null)
        {
            localPos.y = -20f;
            Transform_EarthRock.localPosition = localPos;

            obj.transform.position = Transform_EarthRock.localPosition;
            obj.transform.rotation = Transform_EarthRock.rotation;
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(1.5f);
        obj = Boss_SkillPool.Instance.GetEarthRockFromPool();
        if (obj != null)
        {
            localPos.y = -14f;
            Transform_EarthRock.localPosition = localPos;

            obj.transform.position = Transform_EarthRock.localPosition;
            obj.transform.rotation = Transform_EarthRock.rotation;
            obj.SetActive(true);
        }
        CameraManager.Instance.StartShakeScreen(6, 5, 1);
        yield return new WaitForSeconds(1f);
        animator.SetBool("FouthSkill", false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        if (!IsDead)
        {
            yield return new WaitForSeconds(0.5f);
            int a = Random.Range(0, 4);
            switch (a)
            {
                case 0:
                    animator.SetTrigger("FirstSkill");
                    break;
                case 1:
                    animator.SetTrigger("FirstSkill");
                    break;
                case 2:
                    animator.SetBool("ThirdSkill", true);
                    break;
                case 3:
                    animator.SetBool("FouthSkill", true);
                    break;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<OfflineCharacter>().TakeDamage(1, transform);
        }
    }

    public IEnumerator StartGame()
    {
        sp.color = Color.white;
        SetUpBossFight();
        IsDead = false;
        yield return new WaitForSeconds(2f);
        circleCollider2D.enabled = true;
        StartCoroutine(Move());
    }

}
