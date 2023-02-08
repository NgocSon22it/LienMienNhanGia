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
    [SerializeField] PlayerOffLine Player;

    [Header("Health UI")]
    [SerializeField] Image Health;
    [SerializeField] TMP_Text CurrentHealthTxt;
    [SerializeField] TMP_Text TotalHealthTxt;

    [Header("Health UI")]
    [SerializeField] Image Chakra;
    [SerializeField] TMP_Text CurrentChakraTxt;
    [SerializeField] TMP_Text TotalChakraTxt;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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

    public void SetUpPlayerUI()
    {
        SetUpHealth();
        SetUpChakra();
    }
}
