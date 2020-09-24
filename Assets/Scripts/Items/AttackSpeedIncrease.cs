using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedIncrease : MonoBehaviour, IItem
{
    [SerializeField]
    private float _attackSpeedIncreaseAmount = 0.1f;

    private Item _item;

    public void Activate(GameObject player)
    {
        player.GetComponent<CharacterStats>().AttackSpeed += _attackSpeedIncreaseAmount;
    }

    private void Start()
    {
        _item = GetComponent<Item>();
        _item.ItemAction = this;
    }
}
