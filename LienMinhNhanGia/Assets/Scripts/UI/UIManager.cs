using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject MissionPanel;
    [SerializeField] GameObject PausePanel;

    bool IsPause, IsPlaying = true;

    KeyCode KeyCheck = KeyCode.None;

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
    }

    private void Update()
    {
        ExecutePauseGame();
    }

    public void ControlPauseGame(GameObject Panel, KeyCode key)
    {
        if (Input.GetKeyDown(key) && IsPlaying)
        {
            PauseGame();
            Panel.SetActive(true);
            KeyCheck = key;
            Debug.Log("Pause");
        }
        else if (Input.GetKeyDown(key) && IsPause && key == KeyCheck)
        {
            ResumeGame();
            Panel.SetActive(false);
            KeyCheck = KeyCode.None;
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
    }

}
