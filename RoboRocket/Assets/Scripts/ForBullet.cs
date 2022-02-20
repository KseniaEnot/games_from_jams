using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "PlayerBullet (Clone)")
        {
            if ((collision.name == "StrongTrash(Clone)")|| (collision.name == "Enemy(Clone)"))
            {
                FindObjectOfType<ScoreManager>().ChangeScore(2);
                if (collision.name == "Enemy(Clone)") FindObjectOfType<ScoreManager>().ChangeScore(1);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.name == "Player")
            {
                Destroy(gameObject);
            }
        }
        if((collision.name == "PlayerBullet (Clone)")|| (collision.name == "EnemyBullet (Clone)"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Destr"))
        {
            Destroy(gameObject);
        }
    }
}
