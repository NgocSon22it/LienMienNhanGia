using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level UI")]
    [SerializeField] TMP_Text CurrentLevel;
    [SerializeField] TMP_Text CurrentExpTxt;
    [SerializeField] TMP_Text NextLevelExpTxt;
    [SerializeField] Image CurrentExpBar;

    [Header("Level Variable")]
    private int Level;
    private int Expercience, ExpercienceToNextLevel;

    [Header("Instance")]
    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetUpExperience();
        SetUpExperienceUI();
    }

    public void SetUpExperience()
    {

        Level = AccountManager.Account.Level;
        Expercience = AccountManager.Account.Experience;
        ExpercienceToNextLevel = AccountManager.Account.Level * 100;
        LevelUpReward();
    }

    public void SetUpExperienceUI()
    {
        CurrentLevel.text = "Level: " + Level;
        CurrentExpBar.fillAmount = (float)Expercience / (float)ExpercienceToNextLevel;
        CurrentExpTxt.text = Expercience.ToString();
        NextLevelExpTxt.text = ExpercienceToNextLevel.ToString();
        new AccountDAO().UpdateAccountCoinNLevel(AccountManager.AccountID, AccountManager.Account.Coin, AccountManager.Account.Experience, AccountManager.Account.Level);

    }
    public void AddExperience(int Amount)
    {
        Expercience += Amount;
        while (Expercience >= ExpercienceToNextLevel)
        {
            Level++;
            Expercience -= ExpercienceToNextLevel;
            ExpercienceToNextLevel = Level * 100;
            LevelUpReward();           
        }
        AccountManager.Account.Experience = Expercience;
        AccountManager.Account.Level = Level;
        SetUpExperienceUI();
    }

    public void LevelUpReward()
    {
        OfflinePlayer.Instance.SetJumpTimeMax(Level >= 2 ? 2 : 1);
    }


}
