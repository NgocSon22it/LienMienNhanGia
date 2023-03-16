using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class Online_Shukaku : MonoBehaviourPun, IPunObservable
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
    [SerializeField] Image CurrentHealthUI;
    CircleCollider2D circleCollider2D;
    Animator animator;
    SpriteRenderer sp;
    PhotonView PV;
    GameObject Player;
    public bool IsDead;

    [SerializeField] Transform Transform_GroundSlash;
    [SerializeField] Transform Transform_BeastBomb;
    [SerializeField] Transform Transform_EarthRock;

    private void Start()
    {
        SetUpBossFight();
    }


    public void SetUpHealthUI()
    {
        CurrentHealthUI.fillAmount = (float)CurrentHealth / (float)Health;
    }

    public void SetUpBossFight()
    {
        BossID = "Boss_Shukaku";
        Name = "Shukaku";
        Health = 3000;
        CurrentHealth = 3000;
        Speed = 0;
        Coin_Bonus = 300;
        Experience_Bonus = 300;
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        PV = GetComponent<PhotonView>();
        obj = Boss_SkillPool.Instance.GetBeastBombFromPool();
        CurrentHealthUI.fillAmount = 1f;
        StartCoroutine(Move());

    }
    public void GetTakeDamage(int Damage)
    {
        PV.RPC(nameof(TakeDamage), RpcTarget.AllBuffered, Damage);
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(DamageAnimation());
        SetUpHealthUI();
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;
        obj.SetActive(false);
        StopAllCoroutines();
        gameObject.SetActive(false);

        Online_GameManager.Instance.WinGame();

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
        PV.RPC(nameof(FindClostestPlayer), RpcTarget.AllBuffered);
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
        PV.RPC(nameof(FindClostestPlayer), RpcTarget.AllBuffered);
        Transform_EarthRock.position = new Vector3(Player.transform.position.x, -0.5f, 3);
        Vector3 localPos = Transform_EarthRock.position;
        localPos.x = Player.transform.position.x;

        obj = Boss_SkillPool.Instance.GetFirstRockFromPool();
        if (obj != null)
        {
            localPos.y = -26f;
            Transform_EarthRock.localPosition = localPos;

            obj.transform.position = Transform_EarthRock.localPosition;
            obj.transform.rotation = Transform_EarthRock.rotation;
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(1.5f);
        obj = Boss_SkillPool.Instance.GetEarthRockFromPool();
        if (obj != null)
        {
            localPos.y = -20f;
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
            if (Online_GameManager.Instance.IsStopGame == false)
            {
                yield return new WaitForSeconds(0.5f);
                int a = Random.Range(0, 4);               
                switch (a)
                {
                    case 0:
                        PV.RPC(nameof(CallFirstSkill), RpcTarget.AllBuffered);
                        break;
                    case 1:
                        PV.RPC(nameof(CallFirstSkill), RpcTarget.AllBuffered);
                        break;
                    case 2:
                        PV.RPC(nameof(CallThirdSkill), RpcTarget.AllBuffered);
                        break;
                    case 3:
                        PV.RPC(nameof(CallFouthSkill), RpcTarget.AllBuffered);
                        break;
                }
            }
            else
            {
                Debug.Log("K nha");
                StopAllCoroutines();
            }

        }
    }

    [PunRPC]
    public void CallFirstSkill()
    {
        animator.SetTrigger("FirstSkill");
    }
    [PunRPC]
    public void CallThirdSkill()
    {
        animator.SetBool("ThirdSkill", true);
    }
    [PunRPC]
    public void CallFouthSkill()
    {
        animator.SetBool("FouthSkill", true);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<OnlinePlayer>().TakeDamage(1);
        }
    }

    [PunRPC]
    public void FindClostestPlayer()
    {
        float distanceToClosestPlayer = Mathf.Infinity;
        GameObject closestPlayer = null;
        GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");


        foreach (GameObject currentPlayer in allPlayer)
        {
            float distanceToEnemy = (currentPlayer.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestPlayer)
            {
                distanceToClosestPlayer = distanceToEnemy;
                closestPlayer = currentPlayer;
            }

        }

        Player = closestPlayer;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {


            stream.SendNext(CurrentHealth);
            stream.SendNext(Health);

        }
        else
        {

            CurrentHealth = (int)stream.ReceiveNext();
            Health = (int)stream.ReceiveNext();


        }
    }
}
