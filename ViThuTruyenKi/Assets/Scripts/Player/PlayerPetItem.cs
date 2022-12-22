using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerPetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image PetImage;
    public TMP_Text PetLevel;
    public TMP_Text PetName;

    Pet pet;

    public GameObject HoverPanel;
    
    public void SetUp(Pet _Pet)
    {
        pet = _Pet;
        PetImage.sprite = pet.Image;
        PetName.text = pet.Name.ToString();
        PetLevel.text = "Level " + pet.Level.ToString();

    }

    public void OnlickUpgrade()
    {
        PetBagManager.Instance.MoveToUpgradePanel(pet);
        HoverPanel.SetActive(false);
    }

    public void OnlickDisplay()
    {
        ShopManager.Instance.DisplayInformationSelectedPet(pet);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverPanel.SetActive(false);
    }
}
