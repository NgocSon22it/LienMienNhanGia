using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetBagManager : MonoBehaviour
{
    public static PetBagManager Instance;

    public static List<Pet> Bag = new List<Pet>();

    public GameObject PetItem;
    public Transform Content;

    [Header("EQUIPPET")]
    public Image EquipPetImage;
    public static Pet EquipPet;
    public bool IsEquipPet;
    [SerializeField] GameObject HoverPanel;

    [Header("UPGRADE PET")]
    public Image PetImage;
    public TMP_Text Name;
    public TMP_Text Damage;
    public TMP_Text AttackSpeed;
    public TMP_Text AttackRange;
    public TMP_Text Level;

    [Header("CAN UPGRADE")]
    public TMP_Text NextLevel;
    public TMP_Text NextDamage;
    public TMP_Text NextAttackSpeed;
    public TMP_Text NextAttackRange;
    public TMP_Text UpgradeCost;

    [Header("UPGRADE MANAGER")]
    public GameObject CanUpgradePanel;
    public GameObject MaxLevelPanel;
    public GameObject ListPetPanel;
    public GameObject UpgradePetPanel;

    Pet PetSelected;

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

        foreach (Pet pet in Bag)
        {
            Instantiate(PetItem, Content).GetComponent<PlayerPetItem>().SetUp(pet);
        }
    }

    public void EquipSelectedPet(Pet pet)
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

    public void MoveToUpgradePanel(Pet pet)
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

    public void SetUpStatusForUpgrade(Pet pet)
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
    public void UpgradeSelectedPet(Pet pet)
    {
        pet.Damage += pet.Damage * 30 / 100;
        pet.AttackSpeed += (pet.AttackSpeed * 30 / 100);
        pet.AttackRange += (pet.AttackRange * 30 / 100);
        pet.Level += 1;
        Level.text = "Level " + pet.Level;
        Damage.text = pet.Damage.ToString();
        AttackSpeed.text = pet.AttackSpeed.ToString();
        AttackRange.text = pet.AttackRange.ToString();
        ShopManager.Gold -= 1000;
        SetUpStatusForUpgrade(pet);
        LoadPetList();
    }

    public void UpgradeDisplayPet()
    {
        UpgradeSelectedPet(PetSelected);
    }
}
