using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public float speed = 2;
    float nextChange = 0f;
    float changeY = 0;
    int direction = 0;

    [SerializeField] GameObject bullet;
    private float RateOfFire = 1f;
    float ControlFireSpeed = 0.0f;
    Vector2 whereToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0) direction = 1;
        else direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextChange)
        {
            nextChange = Time.time + 2f;
            changeY = Random.Range(-1f, 1f);
        }
        Vector3 pos = transform.position;
        pos.x += direction*speed * Time.deltaTime;
        pos.y += speed * Time.deltaTime * changeY;

        if (pos.y > Camera.main.orthographicSize - 0.75f) { pos.y = Camera.main.orthographicSize - 0.75f; changeY = -1f; }
        if (pos.y < -Camera.main.orthographicSize + 0.75f) { pos.y = -Camera.main.orthographicSize + 0.75f; changeY = 1f; }

        transform.position = pos;
        if (Time.time > ControlFireSpeed)
        {
            ControlFireSpeed = Time.time + RateOfFire;
            whereToSpawn = new Vector2(transform.position.x, transform.position.y);
            Instantiate(bullet, whereToSpawn, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Destr") Destroy(gameObject);
    }

    public void ChangeDirection(int dir)
    {
        if (dir == 0) direction = -1;
        else direction = dir;
    }
}
