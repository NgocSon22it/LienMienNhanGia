using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;

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

    [SerializeField] TMP_InputField CreateRoom_NameInput;
    [SerializeField] TMP_Dropdown CreateRoom_DropDownNumberPlayerJoin;
    [SerializeField] Transform CreateRoom_BossContent;
    [SerializeField] GameObject CreateRoom_BossItem;

    [SerializeField] Toggle PasswordToggle;
    [SerializeField] TMP_InputField CreateRoom_PasswordInput;
    [SerializeField] GameObject CreateRoom_PasswordPanel;
    bool CreateRoomWithPassword;

    public BossEntity CreateRoom_BossSelected;
    [SerializeField] TMP_Text CreateRoom_CanNotSelectBossItemMessage;
    [SerializeField] TMP_Text CreateRoom_ErrorRoomNameMessage;
    [SerializeField] TMP_Text CreateRoom_ErrorPasswordMessage;

    [Header("Join Room")]
    [SerializeField] GameObject RoomPasswordPanel;
    [SerializeField] TMP_InputField RoomPasswordInput;
    [SerializeField] TMP_Text WrongPasswordMessageTxt;
    RoomInfo SelectedRoom;

    [Header("Test")]
    [SerializeField] TMP_InputField TestId;


    [Header("List Room")]
    public GameObject RoomLobbyItem;
    public Transform RoomContent;

    [Header("List Player")]
    public GameObject PlayerItem;
    public Transform PlayerContent;

    [Header("Extension")]
    string BossExtension = "Boss/";

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
        bossEntities = new BossDAO().GetAllBoss();
        CreateRoom_BossSelected = bossEntities[0];
        SetUpAccountData();
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

    public void CreateRoom_LoadListBossItem()
    {
        foreach (Transform trans in CreateRoom_BossContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < bossEntities.Count; i++)
        {
            bool CheckBossNumber = Account.BossKill >= (i + 1) ? true : false;

            Instantiate(CreateRoom_BossItem, CreateRoom_BossContent).GetComponent<CreateRoom_BossItem>().SetUp(bossEntities[i], CheckBossNumber);
        }
    }

    public void CreateRoom_UpdateSelectedBoss(BossEntity bossEntity)
    {
        CreateRoom_BossSelected = bossEntity;
        CreateRoom_CanNotSelectBossItemMessage.text = "";
        CreateRoom_LoadListBossItem();
    }

    public void CreateRoom_CanNotSelectedBossItem()
    {
        CreateRoom_CanNotSelectBossItemMessage.text = "Bạn chưa mở Boss đó (cần mở cửa boss đó trong chế độ phiêu lưu).";
    }


    public void CreateRoom()
    {
        string roomBossID = CreateRoom_BossSelected.BossID;
        string roomBossName = CreateRoom_BossSelected.Name;
        int NumberPlayer = Convert.ToInt32(CreateRoom_DropDownNumberPlayerJoin.options[CreateRoom_DropDownNumberPlayerJoin.value].text);
        string roomName = CreateRoom_NameInput.text;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = (byte)NumberPlayer;
        roomOptions.BroadcastPropsChangeToAll = true;

        if (roomName.Length > 0)
        {
            CreateRoom_ErrorRoomNameMessage.text = "";
            if (CreateRoomWithPassword)
            {

                string roomPassword = CreateRoom_PasswordInput.text;

                if (roomPassword.Length > 0)
                {
                    roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
                    roomOptions.CustomRoomPropertiesForLobby = new string[] { "Password", "Creator", "Map", "BossName" };
                    roomOptions.CustomRoomProperties.Add("Password", roomPassword);
                    roomOptions.CustomRoomProperties.Add("Creator", PhotonNetwork.NickName);
                    roomOptions.CustomRoomProperties.Add("Map", roomBossID);
                    roomOptions.CustomRoomProperties.Add("BossName", roomBossName);
                    PhotonNetwork.CreateRoom(roomName, roomOptions);
                }
                else
                {
                    CreateRoom_ErrorPasswordMessage.text = "Tên Phòng không được để trống";
                }

            }
            else
            {
                roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
                roomOptions.CustomRoomPropertiesForLobby = new string[] { "Creator", "Map", "BossName" };
                roomOptions.CustomRoomProperties.Add("Creator", PhotonNetwork.NickName);
                roomOptions.CustomRoomProperties.Add("Map", roomBossID);
                roomOptions.CustomRoomProperties.Add("BossName", roomBossName);
                PhotonNetwork.CreateRoom(roomName, roomOptions);
            }
        }
        else
        {
            CreateRoom_ErrorRoomNameMessage.text = "Tên Phòng không được để trống";
        }


    }

    public void JoinRoom(RoomInfo Room)
    {

        if (Room.CustomProperties.ContainsKey("Password"))
        {
            OpenRoomPasswordPanel();
            SelectedRoom = Room;
            Debug.Log("Join Room");
        }
        else
        {
            PhotonNetwork.JoinRoom(Room.Name);
            Debug.Log("Join Room");
        }

    }

    public void OnSubmitRoomPassword()
    {
        if (SelectedRoom.CustomProperties.ContainsKey("Password"))
        {
            if (SelectedRoom.CustomProperties["Password"].ToString() == RoomPasswordInput.text)
            {
                // Join the room
                PhotonNetwork.JoinRoom(SelectedRoom.Name);
            }
            else
            {
                // Show an error message
                WrongPasswordMessageTxt.text = "Sai mật khẩu!";
            }
        }

    }

    public override void OnJoinedRoom()
    {
        NotInroomPanel.SetActive(false);
        InRoomPanel.SetActive(true);
        RoomNameTxt.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("JoinRoom");
        ChatManager.Instance.ConnectToChat();
            

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform trans in PlayerContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(PlayerItem, PlayerContent).GetComponent<PlayerItem>().SetUp(players[i]);
        }

        SetUpMasterClient();
    }

    public bool IsEveryOneReady()
    {
        var players = PhotonNetwork.CurrentRoom.Players;
        foreach (var player in players.Values)
        {
            object isPlayerReady;
            if (player.CustomProperties.TryGetValue("IsReady", out isPlayerReady))
            {
                if ((bool)isPlayerReady == false)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        SetUpMasterClient();
    }

    public void SetUpMasterClient()
    {
        IsReady = PhotonNetwork.IsMasterClient;
        customProperties["IsReady"] = IsReady;
        PhotonNetwork.SetPlayerCustomProperties(customProperties);

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
        bool IsAllReady = IsEveryOneReady();

        if (IsAllReady)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(PhotonNetwork.CurrentRoom.CustomProperties["Map"].ToString());
        }

        
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
        CloseRoomPasswordPanel();
        Onclick_UnReady();
        Debug.Log("LeftRoom");
    }
    public void SetUpAccountData()
    {
        Account = new AccountDAO().GetAccountByID(AccountManager.AccountID);
        Account_SkillU = new Account_SkillDAO().GetAccountSkillbySlotIndex(AccountManager.AccountID, 1);
        Account_SkillI = new Account_SkillDAO().GetAccountSkillbySlotIndex(AccountManager.AccountID, 2);
        Account_SkillO = new Account_SkillDAO().GetAccountSkillbySlotIndex(AccountManager.AccountID, 3);

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
        CreateRoom_NameInput.text = "";
        CreateRoom_PasswordInput.text = "";
        CreateRoom_DropDownNumberPlayerJoin.value = 0;
        CreateRoom_ErrorPasswordMessage.text = "";
        CreateRoom_ErrorRoomNameMessage.text = "";
    }
    public void ResetRoomPasswordData()
    {
        WrongPasswordMessageTxt.text = "";
        RoomPasswordInput.text = "";
    }

    public void OpenRoomPasswordPanel()
    {
        ResetRoomPasswordData();
        RoomPasswordPanel.gameObject.SetActive(true);
    }
    public void CloseRoomPasswordPanel()
    {
        RoomPasswordPanel.gameObject.SetActive(false);

    }
    public void OpenCreateRoomPanel()
    {
        PasswordToggle.isOn = false;
        CreateRoom_LoadListBossItem();
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
        CreateRoom_PasswordPanel.SetActive(status);
        CreateRoomWithPassword = status;
    }
}
