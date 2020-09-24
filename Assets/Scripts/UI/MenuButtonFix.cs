using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonFix : MonoBehaviour
{
    public void DisableButtonOnClick() { gameObject.GetComponent<Button>().interactable = false; }
}
