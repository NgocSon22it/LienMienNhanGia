using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemForMission : MonoBehaviour
{
    [SerializeField] string MissionID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MissionManager.Instance.IncreaseCurrentMission(MissionID);
            Destroy(gameObject);
        }
    }

}
