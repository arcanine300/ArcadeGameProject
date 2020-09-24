using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLife : MonoBehaviour, IItem
{
    private Item _item;

    public void Activate(GameObject player)
    {
        player.GetComponent<PlayerHealth>().AddLife();        
    }

    private void Start()
    {
        _item = GetComponent<Item>();
        _item.ItemAction = this;
    }    
}
