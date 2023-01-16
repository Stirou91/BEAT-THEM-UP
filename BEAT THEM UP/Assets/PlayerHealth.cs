using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] float maxHealth = 100;
    public float currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            // PLAYER DEAD
            GetComponent<PlayerSM>().PlayerDead();
        }

    }
}
