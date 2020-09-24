using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideMobileUI : MonoBehaviour
{
    public Button mobilePausebtn;
    public Joystick joystick1;
    public Joystick joystick2;
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform != RuntimePlatform.Android)
        {
            joystick1.gameObject.SetActive(false);
            joystick2.gameObject.SetActive(false);
            mobilePausebtn.gameObject.SetActive(false);
        }
    }
}
