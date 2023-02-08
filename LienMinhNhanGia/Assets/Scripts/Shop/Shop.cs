using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject GuidePanel;
    [SerializeField] GameObject ShopPanel;
    [SerializeField] Vector3 Offset;
    bool NearPlayer;
    private void Update()
    {
        if(NearPlayer) 
        {
            GuidePanel.transform.position = Camera.main.WorldToScreenPoint(transform.position + Offset);
            if (Input.GetKeyDown(KeyCode.W))
            {
                ShopPanel.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NearPlayer= true;
            GuidePanel.SetActive(true);
            Debug.Log("In");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NearPlayer = false;
            GuidePanel.SetActive(false);
            Debug.Log("Out");

        }
    }
}
