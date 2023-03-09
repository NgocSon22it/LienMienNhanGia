using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shukaku_GroundSlash : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    [SerializeField] GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = transform.right * 30;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gate"))
        {
            Instantiate(Explosion, new Vector2(transform.position.x - 5, transform.position.y + 3), Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
