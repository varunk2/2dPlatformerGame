using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    private int health = 30;

    private void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        RefreshHealth();
    }

    private void RefreshHealth()
    {
        healthText.text = "Health: " + health;
    }

    public void DecreaseHealth(int decrement)
    {
        health -= decrement;
        RefreshHealth();
    }

    public int GetHealth()
    {
        return health;
    }
}
