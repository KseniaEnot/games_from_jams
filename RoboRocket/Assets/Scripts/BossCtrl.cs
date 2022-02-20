using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    [SerializeField] int health;
    int Maxhealth=150;
    int direction = -1;
    float speed = 0.5f;
    float widthorth;
    Vector2 whereToSpawn;
    private bool ifFight = false;
    private float[] Rotate = { 70f, 35f, 0f, -35f, -60f };
    private float[] TentaclesY = { 2.2f, -0.1f, 0f, -0.1f, 2.1f };
    private float[] TentaclesX = { 6.6f, 4.5f, 0f, -4.5f, -6f };
    private bool[] Faza = { true, true, true, true };
    int rand;

    public Sprite[] StrongS = new Sprite[6];
    [SerializeField] GameObject Sield;
    [SerializeField] GameObject StrongTrash;
    [SerializeField] GameObject Tentacles;

    void Start()
    {
        widthorth = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
        Sield.SetActive(true);
        health = Maxhealth;

    }


    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += direction * speed * Time.deltaTime;
        transform.position = pos;
        Debug.Log("Boss:"+health);
        if (ifFight)
        {
            switch (health)
            {
                case 150:
                    if (Faza[0])
                    {
                        Faza[0] = false;
                        Sield.SetActive(true);
                        FindObjectOfType<ToSield>().UpgradeHealth();
                        StartCoroutine(WaveTentaclesCor(2f));
                    }
                    break;
                case 80:
                    if (Faza[1])
                    {
                        Faza[1] = false;
                        Sield.SetActive(true);
                        FindObjectOfType<ToSield>().UpgradeHealth();
                        StartCoroutine(WaveTrashCor(1f));
                    }
                    break;
                case 60:
                    if (Faza[2])
                    {
                        Faza[2] = false;
                        Sield.SetActive(true);
                        FindObjectOfType<ToSield>().UpgradeHealth();
                        StartCoroutine(WaveTentaclesCor(0.5f));
                    }
                    break;
                case 30:
                    if (Faza[3])
                    {
                        Faza[3] = false;
                        Sield.SetActive(true);
                        FindObjectOfType<ToSield>().UpgradeHealth();
                        StartCoroutine(WaveTrashCor(0.3f));
                    }
                    break;
                default:
                    break;
            }
        }
        if (pos.y + 1.5f < Camera.main.orthographicSize) { direction = 0; ifFight = true; }
    }

    IEnumerator WaveTrashCor(float time)
    {
        while((!Faza[1] && Faza[2]) || (!Faza[3] && (health > 0)))
        {
            for (int i = -9; i < 10; i++)
            {
                whereToSpawn = new Vector2(i, transform.position.y);
                Instantiate(StrongTrash, whereToSpawn, Quaternion.identity);
                StrongTrash.GetComponent<SpriteRenderer>().sprite = StrongS[Random.Range(0, StrongS.Length)];
                yield return new WaitForSeconds(time);
            }
        }
    }

    IEnumerator WaveTentaclesCor(float time)
    {
        while((!Faza[0] && Faza[1]) || (!Faza[2] && Faza[3]))
        {
            rand = Random.Range(0, TentaclesX.Length);
            whereToSpawn = new Vector2(transform.position.x, transform.position.y);
            Instantiate(Tentacles, whereToSpawn, Quaternion.identity);
            FindObjectOfType<ToTent>().ChangeRotate(Rotate[rand]);
            FindObjectOfType<ToTent>().ChangeEnd(TentaclesX[rand], TentaclesY[rand]);
            yield return new WaitForSeconds(time);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name == "PlayerBullet (Clone)")&&ifFight)
        {
            health--;
            Destroy(collision.gameObject);
        }
        if (health == 0) StartCoroutine(WaitForDeath());

    }

    IEnumerator WaitForDeath()
    {
        direction = 1;
        yield return new WaitForSeconds(3f);
        FindObjectOfType<LevelManager>().ChangeLevel(5);
        //хлчу чтоб он типа помигал и улетел обратно вверх или что-то такое?
    }
}
