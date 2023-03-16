using Cinemachine;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    OfflinePlayer Player;

    [Header("CheckPoint")]
    CheckPoint checkPoint;

    [Header("Camera BossFight")]
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject BossHealth;
    [SerializeField] GameObject BossShadow;
    [SerializeField] GameObject PlayerCamera;
    [SerializeField] GameObject BossFightCamera; 
    [SerializeField] Transform BossFightCameraTransform;
    bool Move;


    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {

        if (BossFightCamera.transform.position == BossFightCameraTransform.position)
        {
            Move = false;
            BossShadow.SetActive(false);
            Boss.SetActive(true);
            PlayerCamera.SetActive(true);
            BossHealth.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (Move)
        {
            BossFightCamera.transform.position = Vector3.MoveTowards(BossFightCamera.transform.position, BossFightCameraTransform.position, 15f * Time.fixedDeltaTime);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<OfflinePlayer>();
        checkPoint = GetSaveCheckPointByID(AccountManager.Account.CheckPoint);
        Player.transform.position = checkPoint.transform.position;
    }

    public CheckPoint GetSaveCheckPointByID(string CheckPointID)
    {
        CheckPoint checkPoint = null;
        CheckPoint[] allCheckPoint = GameObject.FindObjectsOfType<CheckPoint>();

        foreach (CheckPoint currentCheckPoint in allCheckPoint)
        {
            if (currentCheckPoint.GetCheckPointID().Equals(CheckPointID))
            {
                checkPoint = currentCheckPoint;
            }
        }
        return checkPoint;
    }

    public void ReturnToCheckPoint()
    {
        if (OfflinePlayer.Instance.IsFightBoss)
        {
            NormalCamera();
           
        }       
        Player.GetComponent<OfflinePlayer>().SetCurrentHealth(10);
        Player.GetComponent<OfflinePlayer>().SetCurrentChakra(10);
        checkPoint = GetSaveCheckPointByID(AccountManager.Account.CheckPoint);
        Player.transform.position = checkPoint.transform.position;
        PlayerUIManager.Instance.UpdatePlayerHealthUI();
        PlayerUIManager.Instance.UpdatePlayerChakraUI();
    }


    public void FightBossOffline()
    {
        OfflinePlayer.Instance.IsFightBoss = true;
        PlayerCamera.SetActive(false);
        PlayerCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 30;
        PlayerCamera.GetComponent<CinemachineVirtualCamera>().m_Follow = BossFightCameraTransform;

        BossFightCamera.GetComponent<Camera>().orthographicSize = 30;
        Move = true;
        Boss_Gate.Instance.CloseDoor();
    }

    public void FightBossOnline()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void NormalCamera()
    {
        PlayerCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 20;
        PlayerCamera.GetComponent<CinemachineVirtualCamera>().m_Follow = Player.transform;
        Boss.SetActive(false);
        BossShadow.SetActive(true);
        BossHealth.SetActive(false);
        OfflinePlayer.Instance.IsFightBoss = false;
        Boss_Gate.Instance.OpenDoor();
    }

    private void OnApplicationQuit()
    {
        new AccountDAO().SaveAccountData(AccountManager.Account);
    }

}
