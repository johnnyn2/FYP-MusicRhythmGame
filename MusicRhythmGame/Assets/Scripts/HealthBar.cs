using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    private float health;
    private float startHealth;

    void Start() {
        if(PlayerPrefs.HasKey("Health"))
            startHealth = health = float.Parse(PlayerPrefs.GetString("Health"));
        else
        {
            PlayerPrefs.SetString("Health","100");
            startHealth = health = 100f;
        }
    }

    public void OnTakeDamage(int damage) {
        if (damage > health) {
            return;
        }
        health = health - damage;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().Dead();
        }
    }
}
