using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject GuidePanel;
    [SerializeField] GameObject ShopPanel;
    [SerializeField] Vector3 Offset;
    private void Update()
    {
        if(UIManager.Instance.IsPlayerNearShop == true)
        {
            GuidePanel.transform.position = Camera.main.WorldToScreenPoint(transform.position + Offset);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.IsPlayerNearShop = true;
            GuidePanel.SetActive(true);
            Debug.Log("In");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.IsPlayerNearShop = false;
            GuidePanel.SetActive(false);
            Debug.Log("Out");
        }
    }
}
