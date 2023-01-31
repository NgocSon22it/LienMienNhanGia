using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemForMission : MonoBehaviour
{
    [SerializeField] string ItemName;
    [SerializeField] int MissionID;

    private void Start()
    {
        ItemName = gameObject.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MissionManager.Instance.AddCurrentAmountMission(MissionID);
        }
    }

}
