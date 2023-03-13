using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Shukaku : MonoBehaviour
{
    Animator animator;
    GameObject Player;
    private Quaternion Rotation;

    [SerializeField] Transform Transform_GroundSlash;

    public GameObject SecondSkillObject;
    public GameObject SecondSkillObjectFall;
    public Transform Place2;
    public Transform Place2End;

    public GameObject ThirdSkillObject;
    public Transform Place3;

    public GameObject FouthSkillObject;
    public GameObject FouthSkillEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("FirstSkill");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetBool("FouthSkill", true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("SecondSkill", true);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("ThirdSkill", true);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetBool("FouthSkill", true);
        }
    }


    public void Skill_GroundSlash()
    {
        GameObject groundslash = Boss_SkillPool.Instance.GetGroundSlashFromPool();

        if (groundslash != null)
        {
            groundslash.transform.position = Transform_GroundSlash.position;
            groundslash.transform.rotation = Transform_GroundSlash.rotation;
            groundslash.SetActive(true);
        }
    }

    public void SecondSkill()
    {
        StartCoroutine(ExecuteSecondSkill());
    }
    public void ThirdSkill()
    {
        StartCoroutine(ExecuteThirdSkill());
    }
    public void FouthSkill()
    {
        StartCoroutine(ExecuteFouthSkill());
    }

    IEnumerator ExecuteFouthSkill()
    {

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {
            Vector2 direction = new Vector2(Player.transform.position.x, -25);
            GameObject BulletEffect = Instantiate(FouthSkillEffect, direction, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            Destroy(BulletEffect);
            GameObject BulletIns = Instantiate(FouthSkillObject, direction, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Destroy(BulletIns);
        }
        animator.SetBool("FouthSkill", false);


    }
    IEnumerator ExecuteThirdSkill()
    {

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {
            Vector2 direction = (Vector2)Player.transform.position - (Vector2)Place3.position;
            direction.Normalize();

            GameObject BulletIns = Instantiate(ThirdSkillObject, Place3.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);           
            BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * 3000);
            yield return new WaitForSeconds(1.5f);
        }
        animator.SetBool("ThirdSkill", false);

    }
    IEnumerator ExecuteSecondSkill()
    {
        Instantiate(SecondSkillObject, Place2.position, Quaternion.identity);
        yield return new WaitForSeconds(7.3f);
        animator.SetBool("SecondSkill", false);
        yield return new WaitForSeconds(2.6f);
        for (int i = -2000; i <= 2000; i += 1000)
        {
            GameObject BulletIns = Instantiate(SecondSkillObjectFall, Place2End.position, Quaternion.identity);
            BulletIns.GetComponent<Rigidbody2D>().AddForce(Vector2.right * i);
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<OfflineCharacter>().TakeDamage(1, transform);
        }
    }
}
