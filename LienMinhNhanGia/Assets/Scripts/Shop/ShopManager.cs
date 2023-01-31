using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Instance")]
    public static ShopManager Instance;

    [SerializeField] GameObject ShopPetItem;
    [SerializeField] Transform Content;

    [Header("Shop Manager")]
    [SerializeField] GameObject ListPetItemPanel;
    [SerializeField] GameObject PetInformationPanel;
    [SerializeField] GameObject OwnedText;
    [SerializeField] GameObject NotOwn;

    [Header("Display Information")]
    [SerializeField] Image PetImage;
    [SerializeField] TMP_Text Name;
    [SerializeField] TMP_Text Damage;
    [SerializeField] TMP_Text AttackSpeed;
    [SerializeField] TMP_Text AttackRange;
    [SerializeField] TMP_Text Price;
    [SerializeField] Button BuyBtn;



    PetEntity PetSelected;
    [SerializeField] List<Sprite> ListImage = new List<Sprite>();
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

        List<PetEntity> list = new List<PetEntity>();

        PetEntity Pet1 = new PetEntity(1, "Shukaku", ListImage[0], 10, 0.7f, 15,1, 100);
        PetEntity Pet2 = new PetEntity(2, "Matatabi", ListImage[1], 10, 0.7f, 15,1, 200);
        PetEntity Pet3 = new PetEntity(3, "Isobu", ListImage[2], 10, 0.7f, 15, 1, 300);
        PetEntity Pet4 = new PetEntity(4, "Son Goku", ListImage[3], 10, 0.7f, 15, 1, 400);
        PetEntity Pet5 = new PetEntity(5, "Kokuo", ListImage[4], 10, 0.7f, 15, 1, 500);
        PetEntity Pet6 = new PetEntity(6, "Raijuu", ListImage[5], 10, 0.7f, 15, 1,600);
        PetEntity Pet7 = new PetEntity(7, "Chomei", ListImage[6], 10, 0.7f, 15, 1,700);
        PetEntity Pet8 = new PetEntity(8, "Gyuki", ListImage[7], 10, 0.7f, 15, 1,800);
        PetEntity Pet9 = new PetEntity(9, "Kurama", ListImage[8], 10, 0.7f, 15, 1,900);

        list.Add(Pet1);
        list.Add(Pet2);
        list.Add(Pet3);
        list.Add(Pet4);
        list.Add(Pet5);
        list.Add(Pet6);
        list.Add(Pet7);
        list.Add(Pet8);
        list.Add(Pet9);


        foreach (PetEntity pet in list)
        {
            Instantiate(ShopPetItem, Content).GetComponent<ShopPetItem>().SetUp(pet);
        }
    }
    public void BuySelectedPet(PetEntity pet)
    {
        PetBagManager.Bag.Add(pet);
        LoadPetList();
        SetUpStatusForBuy(pet);
    }

    public void SetUpStatusForBuy(PetEntity pet)
    {
        foreach (PetEntity petInBag in PetBagManager.Bag)
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

    public void DisplayInformationSelectedPet(PetEntity pet)
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
