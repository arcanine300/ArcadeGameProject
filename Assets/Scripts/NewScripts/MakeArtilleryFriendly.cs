using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeArtilleryFriendly : MonoBehaviour, IItem
{
    private Item _item;

    public void Activate(GameObject player)
    {
        GimmickController.Instance.UseItem();
        Destroy(gameObject);
    }

    private void Start()
    {
        _item = GetComponent<Item>();
        _item.ItemAction = this;
    }
}
