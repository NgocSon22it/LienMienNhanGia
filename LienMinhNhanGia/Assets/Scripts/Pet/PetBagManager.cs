using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetBagManager : MonoBehaviour
{
    [Header("Instance")]
    public static PetBagManager Instance;

    public static List<PetEntity> Bag = new List<PetEntity>();
    [SerializeField] GameObject PetItem;
    [SerializeField] Transform Content;

    [Header("EQUIP PET")]
    [SerializeField] Image EquipPetImage;
    public static PetEntity EquipPet;
    private bool IsEquipPet;
    [SerializeField] GameObject HoverPanel;

    [Header("UPGRADE PET")]
    [SerializeField] Image PetImage;
    [SerializeField] TMP_Text Name;
    [SerializeField] TMP_Text Damage;
    [SerializeField] TMP_Text AttackSpeed;
    [SerializeField] TMP_Text AttackRange;
    [SerializeField] TMP_Text Level;

    [Header("CAN UPGRADE")]
    [SerializeField] TMP_Text NextLevel;
    [SerializeField] TMP_Text NextDamage;
    [SerializeField] TMP_Text NextAttackSpeed;
    [SerializeField] TMP_Text NextAttackRange;
    [SerializeField] TMP_Text UpgradeCost;

    [Header("UPGRADE MANAGER")]
    [SerializeField] GameObject CanUpgradePanel;
    [SerializeField] GameObject MaxLevelPanel;
    [SerializeField] GameObject ListPetPanel;
    [SerializeField] GameObject UpgradePetPanel;

    PetEntity PetSelected;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LoadPetList();
    }
    public void LoadPetList()
    {
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (PetEntity pet in Bag)
        {
            Instantiate(PetItem, Content).GetComponent<PlayerPetItem>().SetUp(pet);
        }
    }

    public void EquipSelectedPet(PetEntity pet)
    {
        if (IsEquipPet)
        {
            UnequipPet();
        }
        EquipPet = pet;
        IsEquipPet = true;
        EquipPetImage.sprite = pet.Image;
    }

    public void UnequipPet()
    {
        IsEquipPet = false;
        EquipPet = null;
        HoverPanel.SetActive(false);
        EquipPetImage.sprite = null;
    }

    public void MoveToUpgradePanel(PetEntity pet)
    {
        ListPetPanel.SetActive(false);
        UpgradePetPanel.SetActive(true);
        LoadPetList();
        PetImage.sprite = pet.Image;
        Name.text = pet.Name;
        Level.text = "Level " + pet.Level;
        Damage.text = pet.Damage.ToString();
        AttackSpeed.text = pet.AttackSpeed.ToString();
        AttackRange.text = pet.AttackRange.ToString();
        PetSelected = pet;
        SetUpStatusForUpgrade(pet);      
    }

    public void SetUpStatusForUpgrade(PetEntity pet)
    {
        if (pet.Level == 3)
        {
            MaxLevelPanel.SetActive(true);
            CanUpgradePanel.SetActive(false);

        }
        else
        {
            MaxLevelPanel.SetActive(false);
            CanUpgradePanel.SetActive(true);
            NextLevel.text = "Level " + (pet.Level + 1);
            NextDamage.text = (pet.Damage + (pet.Damage * 30 / 100)).ToString();
            NextAttackSpeed.text = (pet.AttackSpeed + (pet.AttackSpeed * 30 / 100)).ToString();
            NextAttackRange.text = (pet.AttackRange + (pet.AttackRange * 30 / 100)).ToString();
            UpgradeCost.text = "1000";

        }
    }
    public void UpgradeSelectedPet(PetEntity pet)
    {
        pet.Damage += pet.Damage * 30 / 100;
        pet.AttackSpeed += (pet.AttackSpeed * 30 / 100);
        pet.AttackRange += (pet.AttackRange * 30 / 100);
        pet.Level += 1;
        Level.text = "Level " + pet.Level;
        Damage.text = pet.Damage.ToString();
        AttackSpeed.text = pet.AttackSpeed.ToString();
        AttackRange.text = pet.AttackRange.ToString();
        SetUpStatusForUpgrade(pet);
        LoadPetList();
    }

    public void UpgradeDisplayPet()
    {
        UpgradeSelectedPet(PetSelected);
    }
}
