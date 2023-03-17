using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Gate : MonoBehaviour
{
    [SerializeField] GameObject GuidePanel;
    [SerializeField] int MaxVertical, MinVertical;
    [SerializeField] BoxCollider2D TriggerPlayer;
    [SerializeField] List<string> ListMissionID;


    public bool isOpen;
    bool isDetectPlayer;
    Rigidbody2D rb;
    AudioSource audioSource;

    public static Boss_Gate Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isDetectPlayer && !isOpen && CanOpenGate())
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
    public bool CanOpenGate()
    {
        bool checkValue = true;
        if (AccountManager.ListAccountMission.Count > 0)
        {
            foreach (AccountMissionEntity missionEntity in AccountManager.ListAccountMission)
            {
                if (ListMissionID.Contains(missionEntity.MissionID))
                {
                    if(missionEntity.State == 0)
                    {
                        return false;
                    }
                    checkValue = missionEntity.State == 1 ? true : false;
                }
            }
        }
        return checkValue;
    }

    public void OpenDoor()
    {
        audioSource.Play();
        rb.velocity = transform.up * 10;
        TriggerPlayer.enabled = false;
        isOpen = true;
    }

    public void CloseDoor()
    {
        audioSource.Play();
        rb.velocity = -transform.up * 10;
        TriggerPlayer.enabled = true;
        isOpen = false;
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
            AccountManager.UpdateListAccountMission();
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
