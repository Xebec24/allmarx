using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StaminaController : MonoBehaviour
{
    public Image StaminaBar;
    public float stamina;
    public float staminaMax;
    float time;
    float timer = 2;
    public float staminaNeg;

    public void Update()
    {
        bool A = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W);
      
        bool Sprint = Input.GetKey(KeyCode.LeftShift) ^ Input.GetKey(KeyCode.RightShift);
        bool prone = Input.GetKey(KeyCode.Z);
        bool crouch = Input.GetKey(KeyCode.LeftControl) ^ Input.GetKey(KeyCode.LeftControl);
        if (time < timer)
        {
            time += Time.deltaTime;
        }
        if(Sprint && !prone && !crouch && A && stamina >= 0 && time > 2)
        {
            stamina = stamina - staminaNeg;
            time = time - timer;

        }
        StaminaBar.fillAmount = stamina / staminaMax;
    }
      
    
}
