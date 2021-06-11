using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] int startingHealth = 20;
    int currentHealth;
    TMPro.TextMeshProUGUI healthText;
    void Start()
    {
        currentHealth = startingHealth;
        healthText = GetComponent<TMPro.TextMeshProUGUI>();
        UpdateHealth();
    }

    
    public void SubtractHealth()
    {
        currentHealth--;
        UpdateHealth();
    }
    public void AddHealth()
    {
        currentHealth++;
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        healthText.text = currentHealth.ToString();
        if (currentHealth<= 0)
        {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }
}
