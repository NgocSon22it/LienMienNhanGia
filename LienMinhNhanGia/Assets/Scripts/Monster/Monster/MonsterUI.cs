using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    public Slider Slider;
    public Color HighHealth;
    public Color LowHealth;
    public Vector3 Offset;

    public void SetHealth(int Health, int maxHealth)
    {
        Slider.gameObject.SetActive(Health < maxHealth);
        Slider.value = Health;
        Slider.maxValue = maxHealth;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(LowHealth, HighHealth, Slider.normalizedValue);
    }

    private void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
