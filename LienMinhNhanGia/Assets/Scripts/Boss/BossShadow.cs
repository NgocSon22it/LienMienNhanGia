using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShadow : MonoBehaviour
{
    [SerializeField] GameObject GuidePanel;
    [SerializeField] GameObject BossPanel;
    [SerializeField] Vector3 Offset;
    private void Update()
    {
        if (UIManager.Instance.IsPlayerNearBoss == true)
        {
            GuidePanel.transform.position = Camera.main.WorldToScreenPoint(transform.position + Offset);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.IsPlayerNearBoss = true;
            GuidePanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.IsPlayerNearBoss = false;
            GuidePanel.SetActive(false);
        }
    }
}
