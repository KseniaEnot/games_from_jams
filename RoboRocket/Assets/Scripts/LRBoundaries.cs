using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRBoundaries : MonoBehaviour
{
    private float widthorth;

    void Start()
    {
        widthorth = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
        Vector3 posthis = transform.position;
        if (name == "left") posthis.x = -widthorth - 1.5f;
        if (name == "right") posthis.x = +widthorth + 1.5f;
        transform.position = posthis;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player");
            Vector3 pos = other.gameObject.transform.position;
            if (name == "left") {pos.x = widthorth + 0.25f; }
            if (name == "right") { pos.x = -widthorth - 0.25f; }
            //FindObjectOfType<ScoreManager>().ChangeScore(1);
            other.gameObject.transform.position = pos;
        }
    }
}
