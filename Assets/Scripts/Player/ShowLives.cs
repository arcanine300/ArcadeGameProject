using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowLives : MonoBehaviour
{
    public PlayerHealth PlayerHealth;

    private TextMeshProUGUI _livesText;

    private void Start()
    {
        _livesText = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        _livesText.text = $"Lives: {PlayerHealth.Lives}";
    }
}
