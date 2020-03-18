using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    private int health;
    private int startHealth;

    void Start() {
        if(PlayerPrefs.HasKey("Health"))
            startHealth = health = PlayerPrefs.GetInt("Health");
        else
        {
            PlayerPrefs.SetInt("Health", 100);
            startHealth = health = 100;
        }
    }

    public void OnTakeDamage(int damage) {
        if (damage > health) {
            return;
        }
        health = health - damage;
        Debug.Log("Health :" + health);
        Debug.Log("Start Health: " + startHealth);
        Debug.Log("Damage: " + damage);
        healthBar.fillAmount = (float)health / (float)startHealth;

        if (health <= 0) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().Dead();
        }
    }
}
