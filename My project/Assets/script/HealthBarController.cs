using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
     void Update()
     {
        if(health == 0 ^ health < 0)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
     }



}
