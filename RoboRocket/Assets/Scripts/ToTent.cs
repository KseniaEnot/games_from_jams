using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ToTent : MonoBehaviour
{ 
    Vector2 Start1Position;
    Vector2 EndPosition;
    float step = 0.003f;
    float progress = 0;
    // Start is called before the first frame update
    void Start()
    {
        Start1Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(Start1Position, EndPosition, progress);
        progress += step;
        if ((transform.position.x == EndPosition.x)&& (transform.position.y == EndPosition.y))
        {
            Destroy(gameObject);
        }
    }

    public void ChangeRotate(float Z)
    {
        //transform.rotation = Quaternion.Euler(0f, 0f, Z);
        transform.Rotate(0f, 0f, Z);
    }

    public void ChangeEnd(float X,float Y)
    {
        EndPosition = new Vector2(X, Y);
    }
}
