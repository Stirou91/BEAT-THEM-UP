using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject slider;
    [SerializeField] float maxHealth = 100f;
    public float currentHealth;

    Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider = slider.GetComponent<Slider>();
            healthSlider.maxValue = currentHealth;
            healthSlider.value = maxHealth;
        }

    }

    public void TakeDamage(float damageAmount)
    {

        currentHealth -= damageAmount;
        if(healthSlider != null)
        {
            healthSlider.value = currentHealth;

        }

        if (currentHealth <= 0)
        {
            // PLAYER DEAD
            GetComponent<PlayerSM>().PlayerDead();
        } else
        {
            GetComponent<PlayerSM>().PlayerHurt();
        }

    }
}
