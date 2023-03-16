using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Online_GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform SpawnPoint;
    [SerializeField] GameObject Character;
    [SerializeField] GameObject SkyBox;

    [SerializeField] GameObject LosePanel;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject PausePanel;

    public static Online_GameManager Instance;

    public List<GameObject> playerObjects;

    public bool IsStopGame;
    public bool IsWin;
    private void Start()
    {
        PhotonNetwork.Instantiate(Path.Combine("Character/Online/", Character.name), SpawnPoint.position, SpawnPoint.rotation);
        // Find all player game objects and add them to the list
        GetPlayerInGame();
        IsStopGame = false;
        InvokeRepeating(nameof(IsEveryBodyDead), 1f, 5f);
    }

    public void GetPlayerInGame()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Player");
        playerObjects.Clear();
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<OnlinePlayer>().GetCurrentHealth() > 0 && obj != null)
            {
                playerObjects.Add(obj);
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanel.SetActive(true);
        }
    }

    public bool IsEveryBodyDead()
    {
        GetPlayerInGame();
        if (playerObjects.Count > 0)
        {
            foreach (GameObject playerObject in playerObjects)
            {
                OnlinePlayer playerScript = playerObject.GetComponent<OnlinePlayer>();
                if (playerScript.GetCurrentHealth() > 0)
                {
                    IsStopGame = false;
                    return IsStopGame;
                }
            }
            IsStopGame = true;
            IsWin = false;
            SetUpStopGame();
            return IsStopGame;
        }
        else
        {
            IsStopGame = true;
            IsWin = false;
            SetUpStopGame();
            return IsStopGame;
        }
    }

    public void SetUpStopGame()
    {
        if (IsStopGame && !IsWin)
        {
            LosePanel.SetActive(true);
        }
    }

    public void WinGame()
    {
        WinPanel.SetActive(true);
        foreach (GameObject playerObject in playerObjects)
        {
            playerObject.GetComponent<OnlinePlayer>().enabled = false;

        }
        AccountManager.Account.Coin += 300;
    }

    public void QuitGame()
    {
        PhotonNetwork.LeaveRoom(false);
        PhotonNetwork.LoadLevel("Lobby");
        LobbyManager.cachedRoomList.Clear();
    }
    

    private void OnApplicationQuit()
    {
        new AccountDAO().SaveAccountData(AccountManager.Account);
    }

    /*public PolygonCollider2D GetSkyBoxCollider()
    {
        return SkyBox.GetComponent<PolygonCollider2D>();
    }*/
}
