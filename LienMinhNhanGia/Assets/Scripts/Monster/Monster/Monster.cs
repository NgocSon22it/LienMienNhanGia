using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject offlinePlayer;

    protected bool FacingRight = true;

    public void SetUpMonster()
    {
        MonsterEntity monsterEntity = new MonsterDAO().GetMonsterbyId(Monster_ID);
        if (monsterEntity != null)
        {
            Name = monsterEntity.Name;
            Health = monsterEntity.Health;
            Damage = monsterEntity.Damage;
            Speed = monsterEntity.Speed;
            Coin_Bonus = monsterEntity.Coin_Bonus;
            Description = monsterEntity.Description;
            Link_image = monsterEntity.Link_image;

            Debug.Log(Name);
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageAnimation());
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color = Color.red;
        yield return new WaitForSeconds(.2f);
        sp.color = Color.white;
    }
}
