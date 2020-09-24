using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageMultiplier : MonoBehaviour, IItem
{
    [SerializeField]
    private float _damageMultiplierIncreaseAmount = 0.1f;

    private Item _item;

    public void Activate(GameObject player)
    {
        player.GetComponent<CharacterStats>().DamageMultiplier += _damageMultiplierIncreaseAmount;
    }

    private void Start()
    {
        _item = GetComponent<Item>();
        _item.ItemAction = this;
    }
}
