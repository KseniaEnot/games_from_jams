using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCntrl : MonoBehaviour
{
    public AudioSource shoot;
    public AudioSource death;

    public float speed;
    Rigidbody2D rb;
    private Animator playerAnimator;

    [SerializeField] GameObject[] health = new GameObject[3];
    [SerializeField] GameObject bullet;
    private float RateOfFire = 0.5f;
    float ControlFireSpeed = 0.0f;
    Vector2 whereToSpawn;

    bool IsActive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            Vector3 pos = transform.position;

            rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), speed * Input.GetAxis("Vertical"));

            //restrict to top/bottom boundaries
            if (pos.y > Camera.main.orthographicSize) pos.y = Camera.main.orthographicSize;
            if (pos.y < -Camera.main.orthographicSize) pos.y = -Camera.main.orthographicSize;
            transform.position = pos;

            if (Input.GetButton("Jump"))
            {
                playerAnimator.SetBool("IfAtac", true);
                if (Time.time > ControlFireSpeed)
                {
                    shoot.Play();
                    ControlFireSpeed = Time.time + RateOfFire;
                    whereToSpawn = new Vector2(transform.position.x, transform.position.y);
                    Instantiate(bullet, whereToSpawn, Quaternion.identity);
                }
            }
            else { playerAnimator.SetBool("IfAtac", false); }
        }
    }

    public void Active(bool active)
    {
        IsActive = active;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "StrongTrash(Clone)" || collision.gameObject.tag == "Enemy")
        {
            if (SceneManager.GetActiveScene().name == "SandBox")
            {
                death.Play();
                FindObjectOfType<LevelManager>().ChangeLevel(0);
            }
            else
            if (health[0].activeSelf == true)
            {
                health[0].SetActive(false);
            }
            else if (health[1].activeSelf == true)
            {
                health[1].SetActive(false);
            }
            else if (health[2].activeSelf == true)
            {
                health[2].SetActive(false);
                death.Play();
                FindObjectOfType<LevelManager>().ChangeLevel(0);
            }
        }
        if (collision.gameObject.name == "LightTrash(Clone)")
        {
            FindObjectOfType<ScoreManager>().ChangeScore(1);
        }
    }
}
