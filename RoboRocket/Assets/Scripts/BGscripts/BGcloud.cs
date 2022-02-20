using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BGcloud : MonoBehaviour
{
    private float BGspeed = 0.5f;
    private Vector2 offset = Vector2.zero;
    private Material material;
    [SerializeField] Material BossFon;
    private bool IfBoss = true;
    Rigidbody rb;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = material.GetTextureOffset("_MainTex");
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Screen.width / Screen.height;

        if ((gameObject.name == "cloud")||(gameObject.name == "Star"))
        {
            transform.localScale = new Vector3(width, height, -10); 
        }
        else
        {
            rb = GetComponent<Rigidbody>();
            transform.localScale = new Vector3(width, height * 3, -11);
            transform.position = new Vector3(0, 10f, 100);
        }
    }

    public void GoScroll(int lvl)
    {
        float Y = 0;
        switch (lvl)
        {
            case 2: Y = 0; break;
            case 3: Y = -10f; break;
            case 4: Y = -10f; break;
        }
        if (gameObject.name == "BG")
        {
            StartCoroutine(Scroll(Y));
        }
    }

    public void GetBGBoss()
    {
        IfBoss = false;
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Screen.width / Screen.height;
        GetComponent<MeshRenderer>().material= BossFon;
        transform.localScale = new Vector3(width, height, -10);
        transform.position = new Vector3(0, 0, 100);
    }

    IEnumerator Scroll(float y)
    {
        Vector3 NowY = transform.position;
        while ((NowY.y > y)&&IfBoss)
        {
            rb.velocity = new Vector3(0, -BGspeed, 0);
            NowY = transform.position;
            yield return new WaitForSeconds(0f);
        }
        rb.velocity = new Vector3(0, 0, 0);
    }
}
