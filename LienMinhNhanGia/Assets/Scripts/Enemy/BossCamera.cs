using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{
    [SerializeField] GameObject PlayerCamera;
    [SerializeField] GameObject BossFightCamera;

    [SerializeField] Transform BossFightCameraTransform;
    [SerializeField] GameObject BossDoor;

    bool Move;
    GameObject Player;

    BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        
        if (BossFightCamera.transform.position == BossFightCameraTransform.position)
        {
            Move = false;
            Player.GetComponent<PlayerOffLine>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (Move)
        {
            BossFightCamera.transform.position = Vector3.MoveTowards(BossFightCamera.transform.position, BossFightCameraTransform.position, 15f * Time.fixedDeltaTime);
        }
    }

    public void SetUpCamera()
    {
        PlayerCamera.SetActive(false);
        BossFightCamera.GetComponent<Camera>().orthographicSize = 30;
        Move = true;
        boxCollider.enabled = false;
        Player.GetComponent<PlayerOffLine>().enabled = false;
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        BossDoor.GetComponent<BossDoor>().CloseDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetUpCamera();
        }
    }
}
