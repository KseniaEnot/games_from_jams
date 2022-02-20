using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class cloudLooper : MonoBehaviour
{
    public float speedCloud = 0.1f;
    public float speedStar = 0.03f;
    private Vector2 offset = Vector2.zero;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer> ().material;
        offset = material.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "cloud")
        {
            offset.y += speedCloud * Time.deltaTime;
        }
        else
        {
            offset.x += speedStar * Time.deltaTime;
        }
        
        material.SetTextureOffset("_MainTex", offset);
    }
}
