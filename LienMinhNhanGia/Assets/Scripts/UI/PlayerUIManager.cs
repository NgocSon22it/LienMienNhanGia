using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Instance")]
    public static PlayerUIManager Instance;

    [Header("Player")]
    OfflinePlayer Player;

    [Header("Health UI")]
    [SerializeField] Image Health;
    [SerializeField] TMP_Text CurrentHealthTxt;
    [SerializeField] TMP_Text TotalHealthTxt;

    [Header("Chakra UI")]
    [SerializeField] Image Chakra;
    [SerializeField] TMP_Text CurrentChakraTxt;
    [SerializeField] TMP_Text TotalChakraTxt;

    [Header("Skill UI")]
    [SerializeField] Image Skill_U_Image;
    [SerializeField] TMP_Text Skill_U_CostTxt;

    [SerializeField] Image Skill_I_Image;
    [SerializeField] TMP_Text Skill_I_CostTxt;

    [SerializeField] Image Skill_O_Image;
    [SerializeField] TMP_Text Skill_O_CostTxt;

    SkillEntity Skill_U;
    SkillEntity Skill_I;
    SkillEntity Skill_O;

    string Extension = "Skill/";


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<OfflinePlayer>();
        Health.fillAmount = 1f;
        Chakra.fillAmount = 1f;
    }

    public void SetUpHealth()
    {
        CurrentHealthTxt.text = Player.GetCurrentHealth().ToString();
        TotalHealthTxt.text = Player.GetTotalHealth().ToString();
        Health.fillAmount = (float)Player.GetCurrentHealth() / (float)Player.GetTotalHealth();
    }

    public void SetUpChakra()
    {
        CurrentChakraTxt.text = Player.GetCurrentChakra().ToString();
        TotalChakraTxt.text = Player.GetTotalChakra().ToString();
        Chakra.fillAmount = (float)Player.GetCurrentChakra() / (float)Player.GetTotalChakra();
    }

    public void UpdatePlayerHealthUI()
    {
        SetUpHealth();
    }

    public void UpdatePlayerChakraUI()
    {
        SetUpChakra();
    }


}
