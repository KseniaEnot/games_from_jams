using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool InPause;
    int level;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject enemy;
    GameObject[] enemies; Vector3[] enemiesSpeed;
    GameObject[] trash; Vector3[] trashSpeed;
    GameObject[] bullet; Vector3[] bulletSpeed;

    void Update()
    {
        level = FindObjectOfType<LevelManager>().GetLevel();
        if (Input.GetKeyDown(KeyCode.Escape) && level != 4 && level != 0)
        {
            if (!InPause) StartPause();
            else EndPause();
        }
    }

    void StartPause()
    {
        int i = 0;
        PausePanel.SetActive(true);
        FindObjectOfType<SpawnTrash>().ChangeLightLVL(false);
        FindObjectOfType<SpawnTrash>().ChangeStrongLVL(false);
        enemy.SetActive(false);
        if (level == 3) { enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesSpeed = new Vector3[enemies.Length];
            if (enemies != null)
            foreach (GameObject obj in enemies)
            {
                enemiesSpeed[i] = obj.GetComponent<Rigidbody2D>().velocity;
                obj.SetActive(false);
                i++;
            }
        }
        if (level == 1 || level == 2) { trash = GameObject.FindGameObjectsWithTag("Trash");
            trashSpeed = new Vector3[trash.Length];
            i = 0;
            if (trash != null)
            foreach (GameObject obj in trash)
            {
                trashSpeed[i] = obj.GetComponent<Rigidbody2D>().velocity;
                obj.SetActive(false);
                i++;
            }
        }

        bullet = GameObject.FindGameObjectsWithTag("PlayerBullet");
        bulletSpeed = new Vector3[bullet.Length];
        i = 0;
        if (bullet != null) 
            foreach (GameObject obj in bullet)
            {
                bulletSpeed[i] = obj.GetComponent<Rigidbody2D>().velocity;
                obj.SetActive(false);
                i++;
            }

        FindObjectOfType<PlayerCntrl>().Active(false);
        InPause = true;
    }

    public void EndPause()
    {
        PausePanel.SetActive(false);
        int i = 0;

        if (trash != null)
        {
            foreach (GameObject obj in trash)
            {
                obj.SetActive(true);
                obj.GetComponent<Rigidbody2D>().velocity = trashSpeed[i];
                i++;
            }
        }

        i = 0;
        if (bullet != null)
        {
            foreach (GameObject obj in bullet)
            {
                obj.SetActive(true);
                obj.GetComponent<Rigidbody2D>().velocity = bulletSpeed[i];
                i++;
            }
        }

        switch (level)
        {
            case 1:
                FindObjectOfType<SpawnTrash>().ChangeLightLVL(true);
                break;
            case 2:
                FindObjectOfType<SpawnTrash>().ChangeLightLVL(true);
                FindObjectOfType<SpawnTrash>().ChangeStrongLVL(true);
                break;
            case 3:
                FindObjectOfType<SpawnTrash>().ChangeStrongLVL(true);
                enemy.SetActive(true);
                i = 0;
                if (enemies != null)
                foreach (GameObject obj in enemies)
                {
                    obj.SetActive(true);
                    obj.GetComponent<Rigidbody2D>().velocity = enemiesSpeed[i];
                    i++;
                }
                break;
        }
        InPause = false;
        FindObjectOfType<PlayerCntrl>().Active(true);
        trash = null; trashSpeed = null;
        enemies = null; enemiesSpeed = null;
        bullet = null; bulletSpeed = null;
    }
}
