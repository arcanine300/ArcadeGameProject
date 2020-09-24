using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowWeaponTime : MonoBehaviour
{
    public CurrentWeapon CurrentWeapon;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        if (CurrentWeapon.Weapon != null)
        {
            //slider.value = CurrentWeapon.Weapon.GetComponent<Weapon>().TimeLeft / CurrentWeapon.Weapon.GetComponent<Weapon>().ActiveTime;
        }
    }
}
