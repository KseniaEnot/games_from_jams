using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button SandBox;

    void Start()
    {
        if (PlayerPrefs.GetInt("IsPassed") == 0) SandBox.interactable = false;
        else SandBox.interactable = true;
        SandBox.onClick.AddListener(StartGameSandBox);
    }

    public void StartGamePlot()
    {
        SceneManager.LoadScene("MainAdv");
    }

    void StartGameSandBox()
    {
        SceneManager.LoadScene("SandBox");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
