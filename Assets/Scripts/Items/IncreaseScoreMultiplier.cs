using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseScoreMultiplier : MonoBehaviour, IItem
{
    [SerializeField]
    private float _scoreMultiplier = 0.25f;

    private Item _item;

    public void Activate(GameObject player)
    {
        player.GetComponent<CharacterStats>().ScoreMultiplier += _scoreMultiplier;
    }

    private void Start()
    {
        _item = GetComponent<Item>();
        _item.ItemAction = this;
    }
}
