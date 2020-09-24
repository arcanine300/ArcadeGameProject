using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTowersFriendly : MonoBehaviour, IItem
{
    private Item _item;

    private bool _isActive = false;

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
