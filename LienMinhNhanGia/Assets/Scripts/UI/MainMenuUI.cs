using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("IS LOGIN")]
    [SerializeField] GameObject FormMenu;
    [SerializeField] GameObject AllMenu;

    [Header("Player Information")]
    [SerializeField] TMP_Text PlayerNameTxt;
    [SerializeField] TMP_Text PlayerCoinTxt;

    [SerializeField] GameObject OptionPanel;

    public static MainMenuUI Instance;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (AccountManager.AccountID != 0)
        {
            FormMenu.SetActive(false);
            AllMenu.SetActive(true);
        }
        else
        {
            FormMenu.SetActive(true);
            AllMenu.SetActive(false);
        }
        if (UIManager.OutGameToMenu)
        {
            Invoke(nameof(SetUpPlayerInformation), 0f);
        }
    }

    public void LoadMenuForLogin()
    {
        if (AccountManager.AccountID != 0)
        {
            FormMenu.SetActive(false);
            AllMenu.SetActive(true);
        }
        else
        {
            FormMenu.SetActive(true);
            AllMenu.SetActive(false);
        }
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("MainStory");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetDataLogOut()
    {
        OptionPanel.SetActive(false);
    }

    public void SetUpPlayerInformation()
    {
        PlayerNameTxt.text = AccountManager.Account.Name;
        PlayerCoinTxt.text = AccountManager.Account.Coin.ToString();      
    }


}
