using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private Defense _defense;

    public GameObject Ring;

    public GameObject ItemSphere;

    private Renderer _ringMaterial;

    public Material ChangeToColor;

    private bool _activated = false;

    private void Start()
    {
        _defense = GetComponent<Defense>();
        _ringMaterial = Ring.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (_defense.IsActive && !_activated)
        {
            Ring.SetActive(true);
            ItemSphere.SetActive(false);
        }
        if (_defense.IsActive)
        {
            _ringMaterial.material.SetColor("_BaseColor", Color.Lerp(_ringMaterial.material.color, ChangeToColor.color, Time.fixedDeltaTime));
        }
    }
}
