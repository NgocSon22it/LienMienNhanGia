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
    private float Expercience, ExpercienceToNextLevel;

    [Header("Instance")]
    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpExperience();
        SetUpExperienceUI();
    }

    private void Update()
    {
        SetUpExperienceUI();
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddExperience(100);
        }
    }

    public void SetUpExperience()
    {
        Level = 1;
        Expercience = 0;
        ExpercienceToNextLevel = 100;
    }

    public void SetUpExperienceUI()
    {
        CurrentLevel.text = "Level: " + Level;
        CurrentExpBar.fillAmount = Expercience / ExpercienceToNextLevel;
        CurrentExpTxt.text = Expercience.ToString();
        NextLevelExpTxt.text = ExpercienceToNextLevel.ToString();
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
    }

    public void LevelUpReward()
    {
        PlayerOffLine.Instance.SetDoubleJump(Level >= 2 ? true : false);
    }


}
