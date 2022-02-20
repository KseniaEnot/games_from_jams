using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Win()
    {
        PlayerPrefs.SetInt("IsPassed", 1);
        SceneManager.LoadScene("Menu");
    }

    public void Continue()
    {
        FindObjectOfType<Pause>().EndPause();
    }
}
