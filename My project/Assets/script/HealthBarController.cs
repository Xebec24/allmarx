using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image HealthBar;
    public float health;
    public float startHealth;
    public void onTakeDamage (int damage)
    {
        health = health - damage;
        HealthBar.fillAmount = health / startHealth;
    }
}
