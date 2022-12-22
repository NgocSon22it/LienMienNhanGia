using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public GameObject ShopPetItem;
    public Transform Content;

    public GameObject ListPetItemPanel;
    public GameObject PetInformationPanel;
    public GameObject OwnedText;
    public GameObject NotOwn;

    public TMP_Text Goldtxt;

    [Header("DisplayInformation")]
    public Image PetImage;
    public TMP_Text Name;
    public TMP_Text Damage;
    public TMP_Text AttackSpeed;
    public TMP_Text AttackRange;
    public TMP_Text Price;
    public Button BuyBtn;


    public static int Gold = 5000;


    Pet PetSelected;
    public List<Sprite> ListImage = new List<Sprite>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {       
        LoadPetList();
        Goldtxt.text = Gold.ToString();
    }

    public void LoadPetList()
    {
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        List<Pet> list = new List<Pet>();

        Pet Pet1 = new Pet(1, "Shukaku", ListImage[0], 10, 0.7f, 15,1, 100);
        Pet Pet2 = new Pet(2, "Matatabi", ListImage[1], 10, 0.7f, 15,1, 200);
        Pet Pet3 = new Pet(3, "Isobu", ListImage[2], 10, 0.7f, 15, 1, 300);
        Pet Pet4 = new Pet(4, "Son Goku", ListImage[3], 10, 0.7f, 15, 1, 400);
        Pet Pet5 = new Pet(5, "Kokuo", ListImage[4], 10, 0.7f, 15, 1, 500);
        Pet Pet6 = new Pet(6, "Raijuu", ListImage[5], 10, 0.7f, 15, 1,600);
        Pet Pet7 = new Pet(7, "Chomei", ListImage[6], 10, 0.7f, 15, 1,700);
        Pet Pet8 = new Pet(8, "Gyuki", ListImage[7], 10, 0.7f, 15, 1,800);
        Pet Pet9 = new Pet(9, "Kurama", ListImage[8], 10, 0.7f, 15, 1,900);

        list.Add(Pet1);
        list.Add(Pet2);
        list.Add(Pet3);
        list.Add(Pet4);
        list.Add(Pet5);
        list.Add(Pet6);
        list.Add(Pet7);
        list.Add(Pet8);
        list.Add(Pet9);


        foreach (Pet pet in list)
        {
            Instantiate(ShopPetItem, Content).GetComponent<ShopPetItem>().SetUp(pet);
        }
    }
    private void Update()
    {
        Goldtxt.text = Gold.ToString();
    }
    public void BuySelectedPet(Pet pet)
    {
        PetBagManager.Bag.Add(pet);
        Gold -= pet.Price;
        LoadPetList();
        SetUpStatusForBuy(pet);
        Debug.Log(Gold);
    }

    public void SetUpStatusForBuy(Pet pet)
    {
        foreach (Pet petInBag in PetBagManager.Bag)
        {
            if (pet.Id == petInBag.Id)
            {
                OwnedText.SetActive(true);
                NotOwn.SetActive(false);
                break;
            }
            else
            {
                OwnedText.SetActive(false);
                NotOwn.SetActive(true);
            }
        }
    }
    public void BackToListPetItem()
    {
        ListPetItemPanel.SetActive(true);
        PetInformationPanel.SetActive(false);
    }

    public void DisplayInformationSelectedPet(Pet pet)
    {
        ListPetItemPanel.SetActive(false);
        PetInformationPanel.SetActive(true);
        PetSelected = pet;
        PetImage.sprite = pet.Image;
        Name.text = pet.Name;
        Damage.text = pet.Damage.ToString();
        AttackSpeed.text = pet.AttackSpeed.ToString();
        AttackRange.text = pet.AttackSpeed.ToString();
        Price.text = pet.Price.ToString();

        SetUpStatusForBuy(pet);
        
    }

    public void BuyDisplayPet()
    {
        BuySelectedPet(PetSelected);
    }
}
