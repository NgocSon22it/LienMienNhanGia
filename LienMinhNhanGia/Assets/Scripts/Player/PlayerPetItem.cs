using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerPetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image PetImage;
    [SerializeField] TMP_Text PetLevel;
    [SerializeField] TMP_Text PetName;

    PetEntity pet;

    [SerializeField] GameObject HoverPanel;
    
    public void SetUp(PetEntity _Pet)
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

    public void OnlickEquip()
    {
        PetBagManager.Instance.EquipSelectedPet(pet);
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
