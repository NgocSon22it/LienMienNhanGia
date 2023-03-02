using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateRoom_BossItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image BossImage;

    [SerializeField] GameObject CanSelect;
    [SerializeField] GameObject CanNotSelect;
    [SerializeField] GameObject SelectedSquare;

    bool CanSelected;

    [Header("Extension")]
    string Extension = "Boss/";

    BossEntity bossEntity;
    public void SetUp(BossEntity _bossEntity, bool StatusForCanSelect)
    {
        bossEntity = _bossEntity;
        BossImage.sprite = Resources.Load<Sprite>(Extension + bossEntity.BossID);
        SetUpCanSelect(StatusForCanSelect);
        SetUpSelected();
    }

    public void SetUpSelected()
    {
        if (LobbyManager.Instance.CreateRoom_BossSelected.Equals(bossEntity))
        {
            SelectedSquare.SetActive(true);
        }
        else
        {
            SelectedSquare.SetActive(false);
        }
    }

    public void SetUpCanSelect(bool status)
    {
        CanSelected = status;
        CanSelect.SetActive(status);
        CanNotSelect.SetActive(!status);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanSelected)
        {
            LobbyManager.Instance.CreateRoom_UpdateSelectedBoss(bossEntity);
            Debug.Log(bossEntity.Name);
        }
        else
        {
            LobbyManager.Instance.CreateRoom_CanNotSelectedBossItem();
        }
    }
}
