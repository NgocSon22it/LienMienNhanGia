using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] Toggle MusicCheckBox;
    [SerializeField] Toggle SoundCheckBox;

    [SerializeField] AudioMixer MusicAudioMixer;
    [SerializeField] AudioMixer SoundAudioMixer;

    public void ToggleMusic()
    {
        if (MusicCheckBox.isOn)
        {
            MusicAudioMixer.SetFloat("Volume", 0f);
            MainMenuUI.MusicStatus = true;
        }
        else
        {
            MusicAudioMixer.SetFloat("Volume", -80f);
            MainMenuUI.MusicStatus = false;
        }
    }
    public void ToggleSound()
    {
        if (SoundCheckBox.isOn)
        {
            SoundAudioMixer.SetFloat("Volume", 0f);
            MainMenuUI.SoundStatus = true;
        }
        else
        {
            SoundAudioMixer.SetFloat("Volume", -80f);
            MainMenuUI.SoundStatus = false;
        }
    }

    public bool IsStopGame;
    public bool IsWin;
    private void Start()
    {
        MusicCheckBox.isOn = MainMenuUI.MusicStatus;
        SoundCheckBox.isOn = MainMenuUI.SoundStatus;
        PhotonNetwork.Instantiate(Path.Combine("Character/Online/", Character.name), SpawnPoint.position, SpawnPoint.rotation);
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
        new AccountDAO().UpdateAccountOnlineStatus(0, AccountManager.AccountID);
    }

}
