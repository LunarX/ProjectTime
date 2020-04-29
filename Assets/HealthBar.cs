using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health playerHealth;
    public RectTransform RT;

    public void Start()
    {
        playerHealth = GameObject.Find("GameManager").GetComponent<Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
        //RT = GetComponent<RectTransform>();
        //RT.localPosition = new Vector3(Screen.width-100, 100, 0);
        print("X = " + Screen.width);
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}