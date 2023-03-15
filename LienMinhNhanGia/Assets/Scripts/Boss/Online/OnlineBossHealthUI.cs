using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineBossHealthUI : MonoBehaviour
{
    [SerializeField] Image CurrentHealth;
    [SerializeField] Online_Shukaku Boss;

    private void Start()
    {
        CurrentHealth.fillAmount = 1f;
    }

    public void SetUpHealth()
    {
        CurrentHealth.fillAmount = (float)Boss.CurrentHealth / (float)Boss.Health;
    }
}
