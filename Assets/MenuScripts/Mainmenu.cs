using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void ClickToSelectDifficulty()
    {
        SceneManager.LoadScene("SelectMap");
    }

    public void ClickToExit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void ClickMapOne()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ClickReturnMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
        Debug.Log("licked");
    }
}
