using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Sprite[] EnemyS = new Sprite[2];
    private float spawnEnemyRate = 6f;
    float nextSpawnEnemy = 0.0f;
    public GameObject obj;
    float widthorth, RandY;
    Vector2 whereToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        widthorth = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnEnemy)
        {
            nextSpawnEnemy = Time.time + spawnEnemyRate;
            RandY = Random.Range(transform.position.y - 1f, transform.position.y + 1f);
            int RandX = Random.Range(0, 2); if (RandX == 0) RandX = -1;
            whereToSpawn = new Vector2(RandX * transform.position.x, RandY);
            Instantiate(obj, whereToSpawn, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = EnemyS[Random.Range(0, EnemyS.Length)];
        }
    }

    public void ChangeSpawningEnemyRate(float change)
    {
        spawnEnemyRate += change;
    }

    public float GetSpawningEnemyRate()
    {
        return spawnEnemyRate;
    }
}
