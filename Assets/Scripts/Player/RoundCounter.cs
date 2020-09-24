using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    private TextMeshProUGUI _roundCounter;

    private void Start()
    {
        _roundCounter = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        //_roundCounter.text = $"Round {GameManager.Instance.EnemyManager.currentWave}";
    }
}
