using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Photon.Pun.UtilityScripts.TabViewManager;

public class UIManager : MonoBehaviour
{

    GameObject Player;

    [Header("All Panel For PauseGame")]
    [SerializeField] GameObject MissionPanel;
    [SerializeField] GameObject ShopPanel;
    [SerializeField] GameObject BossPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject CommonPanel;

    [Header("All Tab Panel")]
    [SerializeField] List<GameObject> AllTabPanel;

    [Header("Logic")]
    bool IsPause, IsPlaying = true;
    KeyCode KeyCheck = KeyCode.None;
    public bool IsPlayerNearShop;
    public bool IsPlayerNearBoss;


    [Header("Handle Map Tab")]
    [SerializeField] GameObject Map;
    [SerializeField] TMP_Text DoNotHaveMapTxt;
    [SerializeField] Camera MainCamera;
    AccountItemEntity accountItemEntity;
    string MiniMapID = "Item_Minimap";
    string LocateID = "Item_Locate";


    [Header("AccountInformation")]
    [SerializeField] TMP_Text AccountCoinTxt;

    [Header("Instance")]
    public static UIManager Instance;

    public static bool OutGameToMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SetUpUI();
        PlayerBagManager.Instance.InitialManager();
    }

    public void SetUpUI()
    {
        Player.GetComponent<OfflineCharacter>().SetUpPlayer();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPause = true;
        IsPlaying = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPause = false;
        IsPlaying = true;
        KeyCheck = KeyCode.None;
    }

    private void Update()
    {
        ExecutePauseGame();
    }

    public void ControlPauseGame(GameObject Panel, KeyCode key)
    {
        if (IsPlaying)
        {
            PauseGame();
            Panel.SetActive(true);
            KeyCheck = key;
            Debug.Log("Pause");
        }
        else if (IsPause && key == KeyCheck)
        {
            ResumeGame();
            Panel.SetActive(false);
            Debug.Log("Resume");
        }
    }

    public void ExecutePauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ControlPauseGame(PausePanel, KeyCode.Escape);
        }
        else if (Input.GetKeyDown(KeyCode.E) && !IsPlayerNearShop && !IsPlayerNearBoss)
        {
            ControlPauseGame(MissionPanel, KeyCode.E);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            ControlPauseGame(CommonPanel, KeyCode.Tab);
            ResetCommonUIData();
        }
        else if (Input.GetKeyDown(KeyCode.E) && IsPlayerNearShop)
        {
            ControlPauseGame(ShopPanel, KeyCode.E);
            ShopManager.Instance.OpenTabPanel("Item");
        }
        else if (Input.GetKeyDown(KeyCode.E) && IsPlayerNearBoss)
        {
            ControlPauseGame(BossPanel, KeyCode.E);
        }

    }

    public void OpenTabPanel(string tabName)
    {
        UpdateAccountUIData();
        for (int i = 0; i < AllTabPanel.Count; i++)
        {
            AllTabPanel[i].SetActive(false);
        }

        foreach (GameObject obj in AllTabPanel)
        {
            if (obj.name == tabName)
            {
                obj.gameObject.SetActive(true);
            }
        }

        switch (tabName)
        {
            case "Bag":
                PlayerBagManager.Instance.LoadAccountItem();
                break;
            case "Skill":
                SkillManager.Instance.LoadAccountSkillList();
                break;
            case "Map":
                SetUpMiniMap();
                SetUpLocate();
                break;
        }
    }

    public void SetUpMiniMap()
    {
        accountItemEntity = new Account_ItemDAO ().GetAccountItemByItemID(AccountManager.AccountID, MiniMapID);
        if (accountItemEntity != null)
        {
            DoNotHaveMapTxt.text = "";
            Map.SetActive(true);
        }
        else
        {
            DoNotHaveMapTxt.text = "Bạn chưa sở hữu Bản Đồ!";
            Map.SetActive(false);
        }
    }

    public void SetUpLocate()
    {
        accountItemEntity = new Account_ItemDAO().GetAccountItemByItemID(AccountManager.AccountID, LocateID);
        if (accountItemEntity != null)
        {
            Player.gameObject.transform.Find("MinimapIcon").gameObject.SetActive(true);
        }
        else
        {
            Player.gameObject.transform.Find("MinimapIcon").gameObject.SetActive(false);
        }
    }

    public void ResetCommonUIData()
    {
        OpenTabPanel("Bag");      
    }


    public void UpdateAccountUIData()
    {
        AccountCoinTxt.text = AccountManager.Account.Coin.ToString();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("GameMenu");
        ResumeGame();
        OutGameToMenu = true;
        MainMenuUI.Instance.SetUpPlayerInformation();

    }

}
