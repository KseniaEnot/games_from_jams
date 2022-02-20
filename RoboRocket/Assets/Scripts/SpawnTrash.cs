using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{
    public GameObject obj;
    public GameObject obj1;
    float RandX;
    Vector2 whereToSpawn;

    public Sprite[] LightS = new Sprite[9];
    public Sprite[] StrongS = new Sprite[6];
    private float spawnLightRate = 2f;
    float nextSpawnLight = 0.0f;
    private float spawnStrongRate = 3f;
    float nextSpawnStrong = 0.0f;
    bool IfNextStrongLVL = false, IfNextLightLVL = true;
    float widthorth;
    void Start()
    {
        widthorth = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (IfNextLightLVL && (Time.time > nextSpawnLight))
            {
                nextSpawnLight = Time.time + spawnLightRate;
                RandX = Random.Range(-widthorth + 1f, widthorth - 1f);
                whereToSpawn = new Vector2(RandX, transform.position.y);
                Instantiate(obj, whereToSpawn, Quaternion.identity);
                obj.GetComponent<SpriteRenderer>().sprite = LightS[Random.Range(0, LightS.Length)];
            }
        if (IfNextStrongLVL && (Time.time > nextSpawnStrong))
        {
            nextSpawnStrong = Time.time + spawnStrongRate;
                RandX = Random.Range(-widthorth + 1f, widthorth - 1f);
                whereToSpawn = new Vector2(RandX, transform.position.y);
                Instantiate(obj1, whereToSpawn, Quaternion.identity);
                obj1.GetComponent<SpriteRenderer>().sprite = StrongS[Random.Range(0, StrongS.Length)];
        
        }
    }

    public void ChangeSpawningLightRate(float change)
    {
        spawnLightRate += change;
    }

    public float GetSpawningLightRate()
    {
        return spawnLightRate;
    }

    public void ChangeSpawningStrongRate(float change)
    {
        spawnStrongRate += change;
    }

    public float GetSpawningStrongRate()
    {
        return spawnStrongRate;
    }

    public void ChangeStrongLVL(bool swi)
    {
        IfNextStrongLVL = swi;
    }
    public void ChangeLightLVL(bool swi)
    {
        IfNextLightLVL = swi;
    }
}
