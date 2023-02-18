using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Instance")]
    public static LobbyManager Instance;

    [Header("Logic Room")]
    [SerializeField] GameObject NotInroomPanel;
    [SerializeField] GameObject InRoomPanel;

    [Header("In Room")]
    [SerializeField] TMP_Text RoomNameTxt;
    [SerializeField] GameObject StartBtn;
    [SerializeField] GameObject ReadyBtn;
    [SerializeField] GameObject UnReadyBtn;


    [Header("Create Room")]
    [SerializeField] GameObject CreateRoomPanel;

    [SerializeField] TMP_InputField CreateRoomNameInput;
    [SerializeField] TMP_Dropdown DropDownNumberPlayerJoin;


    [Header("Test")]
    [SerializeField] TMP_InputField TestId;

    //Password
    [SerializeField] Toggle PasswordToggle;
    [SerializeField] TMP_InputField CreateRoomPasswordInput;
    [SerializeField] GameObject CreateRoomPasswordPanel;
    bool CreateRoomWithPassword;

    [Header("List Room")]
    public GameObject RoomLobbyItem;
    public Transform RoomContent;

    [Header("List Player")]
    public GameObject PlayerItem;
    public Transform PlayerContent;

    [Header("Find Room")]
    [SerializeField] TMP_Text FindRoomNameTxt;
    [SerializeField] TMP_Dropdown DropDownBoss;

    [Header("DAO Manager")]
    [SerializeField] GameObject DAOManager;

    [Header("Account Information PlayerItem")]
    AccountEntity Account;
    AccountSkillEntity Account_SkillU;
    AccountSkillEntity Account_SkillI;
    AccountSkillEntity Account_SkillO;
    bool IsReady;

    List<BossEntity> bossEntities = new List<BossEntity>();
    private static Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ConnectServer();
        PhotonNetwork.JoinLobby();
        Debug.Log("JoinLobby");

        bossEntities.AddRange(new List<BossEntity>
        {
            new BossEntity("Boss_Shukaku", "Shukaku"),
            new BossEntity("Boss_Matatabi", "Matatabi"),
            new BossEntity("Boss_Isobu", "Isobu")

        });
        DropDownBoss.ClearOptions();
        List<TMP_Dropdown.OptionData> ListOption = new List<TMP_Dropdown.OptionData>();

        foreach (BossEntity bossEntity in bossEntities)
        {
            var option = new TMP_Dropdown.OptionData(bossEntity.BossName, Resources.Load<Sprite>("Boss/" + bossEntity.BossID));
            ListOption.Add(option);
        }
        DropDownBoss.options.Add(new TMP_Dropdown.OptionData("All"));
        DropDownBoss.AddOptions(ListOption);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("JoinLobby");
    }

    public void ConnectServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Connect to Server");
    }

    public void FindRoom()
    {

    }
    public void CreateRoom()
    {
        if (CreateRoomWithPassword)
        {
            string roomName = CreateRoomNameInput.text;
            string roomPassword = CreateRoomPasswordInput.text;
            int NumberPlayer = Convert.ToInt32(DropDownNumberPlayerJoin.options[DropDownNumberPlayerJoin.value].text);

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.MaxPlayers = (byte)NumberPlayer;
            roomOptions.BroadcastPropsChangeToAll = true;

            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
            roomOptions.CustomRoomProperties.Add("password", roomPassword);
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
        else
        {
            string roomName = CreateRoomNameInput.text;
            int NumberPlayer = Convert.ToInt32(DropDownNumberPlayerJoin.options[DropDownNumberPlayerJoin.value].text);

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.MaxPlayers = (byte)NumberPlayer;
            roomOptions.BroadcastPropsChangeToAll = true;

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        Debug.Log("Join Room");
    }

    public override void OnJoinedRoom()
    {
        NotInroomPanel.SetActive(false);
        InRoomPanel.SetActive(true);
        RoomNameTxt.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("JoinRoom");

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform trans in PlayerContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(PlayerItem, PlayerContent).GetComponent<PlayerItem>().SetUp(players[i]);
        }

        StartBtn.SetActive(PhotonNetwork.IsMasterClient);
        ReadyBtn.SetActive(!PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartBtn.SetActive(PhotonNetwork.IsMasterClient);
        ReadyBtn.SetActive(!PhotonNetwork.IsMasterClient);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerItem, PlayerContent).GetComponent<PlayerItem>().SetUp(newPlayer);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in RoomContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }

        foreach (KeyValuePair<string, RoomInfo> entry in cachedRoomList)
        {
            Instantiate(RoomLobbyItem, RoomContent).GetComponent<RoomItem>().SetUp(cachedRoomList[entry.Key]);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("TestOnline");
    }

    public void Onclick_Ready()
    {
        IsReady = true;
        ReadyBtn.gameObject.SetActive(false);
        UnReadyBtn.gameObject.SetActive(true);
        customProperties["IsReady"] = IsReady;
        PhotonNetwork.SetPlayerCustomProperties(customProperties);
    }

    public void Onclick_UnReady()
    {
        IsReady = false;
        ReadyBtn.gameObject.SetActive(true);
        UnReadyBtn.gameObject.SetActive(false);
        customProperties["IsReady"] = IsReady;
        PhotonNetwork.SetPlayerCustomProperties(customProperties);
    }

    public override void OnLeftRoom()
    {
        cachedRoomList.Clear();
        NotInroomPanel.SetActive(true);
        InRoomPanel.SetActive(false);
        CloseCreateRoomPanel();
        Debug.Log("LeftRoom");
    }
    public void SetUpAccountData()
    {
        AccountManager.AccountID = Convert.ToInt32(TestId.text);

        Account = DAOManager.GetComponent<AccountDAO>().GetAccountByID(AccountManager.AccountID);
        Account_SkillU = DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySlotIndex(AccountManager.AccountID, 1);
        Account_SkillI = DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySlotIndex(AccountManager.AccountID, 2);
        Account_SkillO = DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySlotIndex(AccountManager.AccountID, 3);

        PhotonNetwork.NickName = Account.Name;
        // Convert the object to a JSON string
        string AccountJson = JsonUtility.ToJson(Account);
        string Account_SkillU_Json = JsonUtility.ToJson(Account_SkillU);
        string Account_SkillI_Json = JsonUtility.ToJson(Account_SkillI);
        string Account_SkillO_Json = JsonUtility.ToJson(Account_SkillO);

        customProperties["IsReady"] = IsReady;

        customProperties["Account_SkillU"] = Account_SkillU_Json;
        customProperties["Account_SkillI"] = Account_SkillI_Json;
        customProperties["Account_SkillO"] = Account_SkillO_Json;

        customProperties["Account"] = AccountJson;
        PhotonNetwork.SetPlayerCustomProperties(customProperties);
    }
    public void ResetCreateRoomData()
    {
        CreateRoomNameInput.text = "";
        CreateRoomPasswordInput.text = "";
        DropDownNumberPlayerJoin.value = 0;

    }
    public void OpenCreateRoomPanel()
    {
        PasswordToggle.isOn = false;
        CreateRoomPanel.SetActive(true);
        ResetCreateRoomData();        
        SetUpPassword(false);
    }
    public void CloseCreateRoomPanel()
    {
        CreateRoomPanel.SetActive(false);
    }
    public void TogglePasswordChange()
    {
        if (PasswordToggle.isOn)
        {
            SetUpPassword(true);
        }
        else
        {
            SetUpPassword(false);
        }
    }
    public void SetUpPassword(bool status)
    {
        CreateRoomPasswordPanel.SetActive(status);
        CreateRoomWithPassword = status;
    }

    /*public void GetCaptionDropdownMenuValue()
    {
        Sprite a = FindBossDd.options[FindBossDd.value].image;
        Debug.Log(a.name);
    }*/


}
