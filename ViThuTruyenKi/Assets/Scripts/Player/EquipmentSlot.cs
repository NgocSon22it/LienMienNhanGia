using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject HoverPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PetBagManager.EquipPet != null)
        {
            HoverPanel.SetActive(true);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverPanel.SetActive(false);

    }

}
