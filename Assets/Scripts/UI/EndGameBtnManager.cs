using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameBtnManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Destroy(GameManager.Instance); //if returning to the main menu destroy gm so main menu btns are still linked
        //Destroy(GameManager.Instance.Player);
        SceneManager.LoadScene("Menu");
    }
}
