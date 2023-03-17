using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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


    [SerializeField] Toggle MusicCheckBox;
    [SerializeField] Toggle SoundCheckBox;

    [SerializeField] AudioMixer MusicAudioMixer;
    [SerializeField] AudioMixer SoundAudioMixer;

    public static bool MusicStatus = true;
    public static bool SoundStatus = true;

    public void ToggleMusic()
    {
        if (MusicCheckBox.isOn)
        {
            MusicAudioMixer.SetFloat("Volume", 0f);
            MusicStatus = true;
        }
        else
        {
            MusicAudioMixer.SetFloat("Volume", -80f);
            MusicStatus = false;
        }
    }
    public void ToggleSound()
    {
        if (SoundCheckBox.isOn)
        {
            SoundAudioMixer.SetFloat("Volume", 0f);
            SoundStatus = true;
        }
        else
        {
            SoundAudioMixer.SetFloat("Volume", -80f);
            SoundStatus = false;
        }
    }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MusicCheckBox.isOn = MusicStatus;
        SoundCheckBox.isOn = SoundStatus;
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
        new AccountDAO().SaveAccountData(AccountManager.Account);
        new AccountDAO().UpdateAccountOnlineStatus(0, AccountManager.AccountID);
    }

    public void SetUpPlayerInformation()
    {
        PlayerNameTxt.text = AccountManager.Account.Name;
        PlayerCoinTxt.text = AccountManager.Account.Coin.ToString();      
    }

    private void OnApplicationQuit()
    {
        new AccountDAO().SaveAccountData(AccountManager.Account);
        new AccountDAO().UpdateAccountOnlineStatus(0, AccountManager.AccountID);
    }

}
