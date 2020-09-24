using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowDefenseTime : MonoBehaviour
{
    public CurrentDefense Defense;

    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        if (Defense.Defense != null)
        {
            _slider.value = Defense.Defense.GetComponent<Defense>().TimeLeft / Defense.Defense.GetComponent<Defense>().ActiveTime;
        }
    }
}
