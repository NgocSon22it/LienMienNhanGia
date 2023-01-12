using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Enemy Boss;
    [SerializeField] Image CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth.fillAmount = Boss.CurrentHealthPoint / (float)Boss.TotalHealthPoint;
    }
}
