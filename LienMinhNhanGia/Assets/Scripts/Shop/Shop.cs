using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject ShopPanel;

    bool NearPlayer;


    private void Update()
    {
        if(NearPlayer) 
        {
            if(Input.GetKeyDown(KeyCode.W))
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NearPlayer = false;
        }
    }
}
