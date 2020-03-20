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
        health = 100;
        startHealth = 100;
    }

    public void OnTakeDamage(int damage) {
        if (damage > health) {
            return;
        }
        health = health - damage;
        healthBar.fillAmount = health / startHealth;
        if (health <= 60)
            healthBar.color = new Color32(231,163,32,255);
        if (health <= 30) 
            healthBar.color = new Color32(255,0,0,255);
        if (health <= 0) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().Dead();
        }
    }
}
