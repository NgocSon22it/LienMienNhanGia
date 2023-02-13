using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerOffLine Player;

    [Header("All Panel For PauseGame")]
    [SerializeField] GameObject MissionPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject CommonPanel;

    [Header("All Tab Panel")]
    [SerializeField] List<GameObject> AllTabPanel;

    [Header("Logic")]
    bool IsPause, IsPlaying = true;
    KeyCode KeyCheck = KeyCode.None;


    [Header("DAOManager")]
    [SerializeField] GameObject DAOManager;

    [Header("Instance")]
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetUpUI();
        PlayerBagManager.Instance.InitialManager();
        ShopManager.Instance.InitialManager();
    }

    public void SetUpUI()
    {
        Player.SetUpPlayer();
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
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ControlPauseGame(MissionPanel, KeyCode.E);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            ControlPauseGame(CommonPanel, KeyCode.Tab);
        }

    }

    public void OpenTabPanel(int tab)
    {
        for (int i = 0; i < AllTabPanel.Count; i++)
        {
            AllTabPanel[i].SetActive(false);
        }
        AllTabPanel[tab - 1].SetActive(true);
    }


}
