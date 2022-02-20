using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score;
    Text ScoreDisplay;
    string curScene;
    private int[] lvl = { 35, 75, 150 };
    private int[] lvlSand = { 40, 100, 170 };
    private bool[] IfGetLvl = { false, false, false };

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SandBox")
        {
            for (int i = 0; i < lvl.Length; i++)
            {
                lvl[i] = lvlSand[i];
            }
        }
        score = 0;
        ScoreDisplay = GetComponent<Text>();
        curScene = SceneManager.GetActiveScene().name;
        //ScoreDisplay.text = score.ToString();
    }

    public void ChangeScore(int add)
    {
        score += add;
        ScoreDisplay.text = score.ToString();
        if (score % 10 == 0)
        {
            if (FindObjectOfType<SpawnTrash>().GetSpawningLightRate() > 0.8f)
            {
                FindObjectOfType<SpawnTrash>().ChangeSpawningLightRate(-0.1f);
            }
            if (FindObjectOfType<SpawnTrash>().GetSpawningStrongRate() > 0.8f)
            {
                FindObjectOfType<SpawnTrash>().ChangeSpawningStrongRate(-0.1f);
            }
            if (FindObjectOfType<SpawnEnemy>().GetSpawningEnemyRate() > 1f)
            {
                FindObjectOfType<SpawnEnemy>().ChangeSpawningEnemyRate(-0.1f);
            }
        }
        if ((score < lvl[1]) && (score >= lvl[0]) && (!IfGetLvl[0])) { FindObjectOfType<LevelManager>().ChangeLevel(2); IfGetLvl[0] = true; }
        if ((score < lvl[2]) && (score >= lvl[1]) && (!IfGetLvl[1])){ FindObjectOfType<LevelManager>().ChangeLevel(3); IfGetLvl[1] = true; }
        if ((score >= lvl[2]) && curScene == "MainAdv" && (!IfGetLvl[2])) { FindObjectOfType<LevelManager>().ChangeLevel(4);IfGetLvl[2] = true; }
    }

    public int GetScore()
    {
        return score;
    }
}
