using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] float baseHealth = 3.0f;
    
    [SerializeField] GameObject deathVFX;
    [SerializeField] float deathVFXDelay = 1f;
    [SerializeField] Vector3 deathVFXOffset = new Vector3(0f, 0f, 0f);

    [SerializeField] float currentHealth = 0.0f;

    void Start()
    {
        currentHealth = baseHealth;
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
    }
    public void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            TriggerDeathVFX();
            Destroy(gameObject);
        }
    }

    private void TriggerDeathVFX()
    {
        CheckDeathVFX();
    }
    private void CheckDeathVFX()
    {
        if (!deathVFX)
        {
            return;
        }
        GameObject deathVFXObject = Instantiate(deathVFX, transform.position + deathVFXOffset, transform.rotation);
        Destroy(deathVFXObject, deathVFXDelay);
    }
}
