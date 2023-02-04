using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrucThu : MonoBehaviour
{
    [SerializeField] GameObject MissionPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ControlPauseGame(MissionPanel, KeyCode.E);
            Debug.Log(MissionPanel.gameObject.name);
            Destroy(gameObject);
        }
    }
}
