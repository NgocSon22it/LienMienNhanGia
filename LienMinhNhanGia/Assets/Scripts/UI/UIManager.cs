using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerOffLine Player;



    [SerializeField] GameObject MissionPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject MapPanel;

    bool IsPause, IsPlaying = true;

    KeyCode KeyCheck = KeyCode.None;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetUpUI();
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
            ControlPauseGame(MapPanel, KeyCode.Tab);
        }
        
    }

}
