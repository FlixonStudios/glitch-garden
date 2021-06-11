using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    [SerializeField] int startingStars = 100;
    int currentStars;
    TMPro.TextMeshProUGUI starText;

    void Start()
    {
        InitialiseStarDisplay();
        UpdateStarDisplay();
    }

    private void InitialiseStarDisplay()
    {
        starText = GetComponent<TextMeshProUGUI>();
        currentStars = startingStars;
    }

    private void Update()
    {

    }

    private void UpdateStarDisplay()
    {
        starText.text = currentStars.ToString();
    }
    public void AddStars(int starsToAdd)
    {
        currentStars += starsToAdd;
        UpdateStarDisplay();
    }
    public void SpendStars(int starsToRemove)
    {
        if (starsToRemove <= currentStars)
        {
            currentStars -= starsToRemove;
            UpdateStarDisplay();
        }
    }
    public bool HaveEnoughStars(int amount)
    {
        return currentStars >= amount;
    }       
}
