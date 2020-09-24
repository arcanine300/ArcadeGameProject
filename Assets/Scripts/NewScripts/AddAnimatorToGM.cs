using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnimatorToGM : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.UIAnimator = GetComponent<Animator>();
    }
}
