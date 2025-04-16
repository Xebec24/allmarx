using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonController : MonoBehaviour
{
    public HealthBarController healthbar;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            if(healthbar)
            {
                healthbar.onTakeDamage(10);
            }
        }
    }
}
