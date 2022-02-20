using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSield : MonoBehaviour
{
    private int SieldH;
    private int MaxH = 20;

    void Start()
    {
        SieldH = MaxH;
    }

    public void UpgradeHealth()
    {
        SieldH = MaxH;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PlayerBullet (Clone)")
        {
            SieldH--;
            Destroy(collision.gameObject);
        }
        if (SieldH == 0) gameObject.SetActive(false);

    }
}
