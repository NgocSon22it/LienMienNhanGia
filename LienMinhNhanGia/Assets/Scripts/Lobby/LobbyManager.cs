using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using System;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Instance")]
    public static LobbyManager Instance;

    [Header("Logic Room")]
    [SerializeField] GameObject NotInroomPanel;
    [SerializeField] GameObject InRoomPanel;

    [Header("In Room")]
    [SerializeField] TMP_Text RoomNameTxt;


    [Header("Create Room")]
    [SerializeField] GameObject CreateRoomPanel;
   
    [SerializeField] TMP_InputField CreateRoomNameInput;
    [SerializeField] TMP_Dropdown DropDownNumberPlayerJoin;

    //Password
    [SerializeField] Toggle PasswordToggle;
    [SerializeField] TMP_InputField CreateRoomPasswordInput;
    [SerializeField] GameObject CreateRoomPasswordPanel;
    bool CreateRoomWithPassword;

    [Header("List Room")]
    public GameObject RoomLobbyItem;
    public Transform RoomContent;

    [Header("Find Room")]
    [SerializeField] TMP_Text FindRoomNameTxt;
    [SerializeField] TMP_Dropdown DropDownBoss;

    List<BossEntity> bossEntities = new List<BossEntity>();
    private static Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

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
        PhotonNetwork.NickName = "Ngoc Son";
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
            roomOptions.MaxPlayers = (byte) NumberPlayer;
            
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
            roomOptions.CustomRoomProperties.Add("password", roomPassword);

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
        else
        {

        }    
    }

    public override void OnJoinedRoom()
    {
        NotInroomPanel.SetActive(false);
        InRoomPanel.SetActive(true);
        RoomNameTxt.text = PhotonNetwork.CurrentRoom.Name;
        Debug.Log("JoinRoom");

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

    public void OpenCreateRoomPanel()
    {
        CreateRoomPanel.SetActive(true);
    }

    public void CloseCreateRoomPanel()
    {
        CreateRoomPanel.SetActive(false);
    }

    public void TogglePasswordChange()
    {
        if(PasswordToggle.isOn)
        {
            CreateRoomPasswordPanel.SetActive(true);
            CreateRoomWithPassword = true;
        }
        else
        {
            CreateRoomPasswordPanel.SetActive(false);
            CreateRoomWithPassword = false;
        }
    }


    /*public void GetCaptionDropdownMenuValue()
    {
        Sprite a = FindBossDd.options[FindBossDd.value].image;
        Debug.Log(a.name);
    }*/

}
