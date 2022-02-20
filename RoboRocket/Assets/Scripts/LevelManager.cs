using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private int curLevel = 1;
    public GameObject clouds;
    public GameObject Star;
    [SerializeField] GameObject enemy;
    [SerializeField] Text instructions;
    [SerializeField] GameObject End;
    [SerializeField] GameObject Win;
    GameObject Boss = null;

    void Start()
    {
        curLevel = -1;
        instructions.text = "Наша планета погрязла в мусоре. Твоя цель собирать или уничтожать его!";
        StartCoroutine(WaitForNextLevel(curLevel));
        enemy.SetActive(false);
        if (SceneManager.GetActiveScene().name == "MainAdv")
        {
            Boss = GameObject.Find("Boss");
            Boss.SetActive(false);
        }
    }

    public void ChangeLevel(int change)
    {
        curLevel = change;
        if (change != 0) StartCoroutine(WaitForNextLevel(curLevel));

        switch (curLevel)
        {
            case 0:
                StartCoroutine(WaitForEndGame());
                break;
            case 2:
                instructions.text = "Входим в верхние слоя атмосферы. Ожидается появление неперерабатываемого мусора. ";
                instructions.text += "При столкновении возможны необратимые повреждения.";
                clouds.SetActive(false);

                break;
            case 3:
                instructions.text = "Входим на орбиту! Будь готов к бою!";
                break;
            case 4:
                instructions.text = "ТРЕВОГА!!!";
                FindObjectOfType<BGcloud>().GetBGBoss();
                Star.SetActive(true);
                break;
            case 5:
                //win!!!!!!
                Win.SetActive(true);
                break;
            default: break;
        }
    }

    void SaveScore(int score)
    {
        string key = "Record";
        for (int rec = 1; rec < 6; rec++)
        {
            if (PlayerPrefs.HasKey(key + rec.ToString()))
            {
                if (score > PlayerPrefs.GetInt(key + rec.ToString()))
                {
                    for (int j = 5; j > rec; j--)
                    {
                        if (PlayerPrefs.HasKey(key + (j-1).ToString())) { int sc = PlayerPrefs.GetInt(key + (j - 1).ToString());
                            PlayerPrefs.SetInt(key + j.ToString(), sc); }
                    }
                    PlayerPrefs.SetInt(key + rec.ToString(), score);
                    break;
                }
            }
            else { PlayerPrefs.SetInt(key + rec.ToString(), score); break; }
        }
        PlayerPrefs.Save();
    }

    IEnumerator WaitForNextLevel(int curLevel)
    {
        FindObjectOfType<SpawnTrash>().ChangeLightLVL(false);
        FindObjectOfType<SpawnTrash>().ChangeStrongLVL(false);
        enemy.SetActive(false);
        FindObjectOfType<BGcloud>().GoScroll(curLevel);
        yield return new WaitForSeconds(5f);

        instructions.text = "";
        if (curLevel == -1)
        {
            instructions.text = "Управление на стрелки или AWSD. Стрельба на пробел.";
            yield return new WaitForSeconds(4f);
            instructions.text = "";
            FindObjectOfType<SpawnTrash>().ChangeLightLVL(true);
        }
        if (curLevel == 2) {
            //light + strong
            FindObjectOfType<SpawnTrash>().ChangeLightLVL(true);
            FindObjectOfType<SpawnTrash>().ChangeStrongLVL(true);
        }
        if (curLevel == 3)
        {
            //strong + enemy
            enemy.SetActive(true);
            FindObjectOfType<SpawnTrash>().ChangeStrongLVL(true);
        }
        if (curLevel == 4) {
            //BOSS FIGHT
            Boss.SetActive(true);
            Debug.Log("4 level");  
        }
    }

    IEnumerator WaitForEndGame()
    {
        int cur_score = FindObjectOfType<ScoreManager>().GetScore();
        if (SceneManager.GetActiveScene().name == "SandBox") SaveScore(cur_score);

        enemy.SetActive(false);
        FindObjectOfType<SpawnTrash>().ChangeLightLVL(false);
        FindObjectOfType<SpawnTrash>().ChangeStrongLVL(false);
        End.SetActive(true);
        yield return new WaitForSeconds(200f);
        /*instructions.text = "перезапуск уровня...";
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);*/
    }

    public int GetLevel()
    {
        return curLevel;
    }
}
