using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DisplayScore : MonoBehaviour
{
    public CharacterStats PlayerStats;

    public TextMeshProUGUI ScoreText;

    public TextMeshProUGUI ScoreMultiplierText;

    private void Update()
    {
        if (ScoreText != null)
        {
            ScoreText.text = $"Score : {(int)GameManager.Instance.PlayerScore}";
        }

        if (ScoreMultiplierText != null && PlayerStats != null)
        {
            ScoreMultiplierText.text = $"x{Math.Round(PlayerStats.ScoreMultiplier, 2)}";
        }               
    }
}
