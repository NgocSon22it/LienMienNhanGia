using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] string CheckPointID;

    public string GetCheckPointID()
    {
        return CheckPointID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AccountManager.Account.CheckPoint = CheckPointID;
        }
    }

}
