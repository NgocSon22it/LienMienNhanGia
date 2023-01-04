using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPetItem : MonoBehaviour
{

    public Image PetImage;
    public TMP_Text PetPrice;
    public TMP_Text PetName;

    public GameObject OwnedText;
    public GameObject NotOwn;



    PetEntity pet;

    public void SetUp(PetEntity _Pet)
    {
        pet = _Pet;
        PetImage.sprite = pet.Image;
        PetPrice.text = pet.Price.ToString();
        PetName.text = pet.Name.ToString();

        foreach (PetEntity pet in PetBagManager.Bag)
        {
            if (_Pet.Id == pet.Id)
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

    public void Onlick()
    {
        ShopManager.Instance.BuySelectedPet(pet);
    }

    public void OnlickDisplay()
    {
        ShopManager.Instance.DisplayInformationSelectedPet(pet);
    }
}
