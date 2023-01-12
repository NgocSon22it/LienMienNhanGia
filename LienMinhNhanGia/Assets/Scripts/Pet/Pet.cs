using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Pet : MonoBehaviour
{
    [Header("Common Value")]
    [SerializeField] int Damage, AttackRange;
    [SerializeField] float AttackSpeed;

    private Quaternion Rotation;
    [SerializeField] GameObject Player;


    float posX, posY, angle = 1.5f;
    float rotationRadius = 2f;
    float angularSpeed = 2f;
    [SerializeField] Transform rotationCenter;
    public void SetUp(PetEntity petEntity)
    {
        Damage = petEntity.Damage;
        AttackRange = petEntity.AttackRange;
        AttackSpeed = petEntity.AttackSpeed;
    }

    private void Start()
    {
        float ATSP = 1f;
        Debug.Log(ATSP);
        InvokeRepeating("Fire", 2f, ATSP);
    }

    private void FixedUpdate()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y;
        transform.position = new Vector2(posX, posY + 8);

        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360f)
        {
            angle = 1.5f;
        }
    }
    public void Fire()
    {
        Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position;
        direction.Normalize();

        GameObject BulletIns = BulletPool.Instance.GetBulletFromPool();
        if (BulletIns != null)
        {
            BulletIns.transform.position = transform.position;
            BulletIns.SetActive(true);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Rotation.eulerAngles = new Vector3(0, 0, angle);
            BulletIns.transform.rotation = Rotation;
            BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * 2000);
        }

    }
}
