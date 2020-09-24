using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowFPS : MonoBehaviour
{
    public TextMeshProUGUI FpsText;

    public bool _isDelay;

    IEnumerator Delay()
    {
        _isDelay = true;
        int fps = (int)(1f / Time.unscaledDeltaTime);
        FpsText.text = fps.ToString();
        yield return new WaitForSeconds(0.1f);
        _isDelay = false;
    }

    private void Update()
    {
        if (!_isDelay)
        {
            StartCoroutine(Delay());
        }
    }
}
