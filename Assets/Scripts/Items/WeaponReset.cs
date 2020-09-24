using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReset : MonoBehaviour, IItem
{
    private Item _item;

    public void Activate(GameObject player)
    {
        player.GetComponent<PlayerParts>().ShootWeapon.CurrentWeapon.GetComponent<Weapon>().ResetTimer();
    }

    private void Start()
    {
        _item = GetComponent<Item>();
        _item.ItemAction = this;
    }
}
