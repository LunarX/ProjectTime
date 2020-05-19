using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;
    public RectTransform RT;
    public Gradient gradient;
    public Image fill;

    public void Start()
    {
        playerHealth = GameObject.Find("GameManager").GetComponent<Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
        
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;

        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }
}