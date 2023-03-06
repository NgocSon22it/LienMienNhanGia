using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Gate : MonoBehaviour
{
    [SerializeField] GameObject GuidePanel;
    [SerializeField] int MaxVertical, MinVertical;
    [SerializeField] BoxCollider2D TriggerPlayer;
    [SerializeField] int MapIndex;
    [SerializeField] int NumberMission;


    bool isOpen;
    bool isDetectPlayer;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (isDetectPlayer && !isOpen && MissionManager.Instance.MissionCount == NumberMission)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                OpenDoor();
            }
        }
        if (transform.position.y >= MaxVertical && isOpen)
        {
            transform.position = new Vector2(transform.position.x, MaxVertical);
            rb.velocity = Vector2.zero;
        }
        else if (transform.position.y <= MinVertical && !isOpen)
        {
            transform.position = new Vector2(transform.position.x, MinVertical);
            rb.velocity = Vector2.zero;
        }
    }

    public void OpenDoor()
    {
        rb.velocity = transform.up * 10;
        TriggerPlayer.enabled = false;
        isOpen = true;
        //CameraManager.Instance.StartShakeScreen(10, 3, 2);
    }

    public void CloseDoor()
    {
        rb.velocity = -transform.up * 10;
        TriggerPlayer.enabled = false;
        isOpen = false;
        //CameraManager.Instance.StartShakeScreen(10, 3, 2);
    }

    public void ShowGuidePanel()
    {
        GuidePanel.SetActive(true);
        isDetectPlayer = true;
    }

    public void HideGuidePanel()
    {
        GuidePanel.SetActive(false);
        isDetectPlayer = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowGuidePanel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HideGuidePanel();
        }
    }
}
