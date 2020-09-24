using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    public GameObject MobilePauseBtn;

    public KeyCode EscapeKey;

    private bool _isActive = false;

    private RotateTowardsCursor lookScript;
    private GameObject currWeapon;

    private void Start()
    {
        lookScript = GameObject.Find("Input Controller").GetComponent<RotateTowardsCursor>();
        //currWeapon = GameObject.Find("Current Weapon");
    }

    private void Update()
    {
        if (Input.GetKeyDown(EscapeKey))
        {
            _isActive = !_isActive;

            if (_isActive)
            {
                Time.timeScale = 0;
                if (lookScript) { lookScript.enabled = false; }
                if (currWeapon) { currWeapon.SetActive(false); }
                PauseMenuPanel.SetActive(true);
            }
            else
            {
                DisablePauseMenu();
            }
        }        
    }

    public void EnablePauseMenu()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) 
        {
            if (MobilePauseBtn)
            {
                MobilePauseBtn.SetActive(false);
            }
        }

        Time.timeScale = 0;
        if (lookScript) { lookScript.enabled = false; }
        if (currWeapon) { currWeapon.SetActive(false); }
        PauseMenuPanel.SetActive(true);
    }

    public void DisablePauseMenu()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (MobilePauseBtn)
            {
                MobilePauseBtn.SetActive(true);
            }
        }
        if (lookScript) { lookScript.enabled = true; }
        if (currWeapon) { currWeapon.SetActive(true); }
        Time.timeScale = 1;
        PauseMenuPanel.SetActive(false);
        _isActive = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        Destroy(GameManager.Instance); //if returning to the main menu destroy gm so main menu btns are still linked
        Destroy(GameManager.Instance.Player);
        SceneManager.LoadScene("Menu");
    }
}
