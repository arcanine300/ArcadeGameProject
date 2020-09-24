using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempIncreaseMoveSpeed : MonoBehaviour, IItem
{
    [SerializeField]
    private float _moveSpeed = 2f;

    private Item _item;

    private CharacterStats _stats;

    public void Activate(GameObject player)
    {
        player.GetComponent<CharacterStats>().MoveSpeed += _moveSpeed;
    }

    private void Start()
    {
        _item = GetComponent<Item>();
        _item.ItemAction = this;
    }
}
