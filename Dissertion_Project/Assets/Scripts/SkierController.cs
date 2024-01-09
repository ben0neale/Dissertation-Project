using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkierController : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] List<GameObject> Graphics;
    GameObject Player;
    Rigidbody2D RB;
    public float minSpeed, maxSpeed;
    public float Yspeed;
    int direction;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody2D>();
        if (Player.transform.position.x < transform.position.x)
        {
            direction = -1;
            Graphics[0].SetActive(true);
            Graphics[1].SetActive(false);
        }

        else
        {
            direction = 1;
            Graphics[0].SetActive(false);
            Graphics[1].SetActive(true);
        }
        RB.velocity = new Vector2(Random.Range(minSpeed, maxSpeed) * direction, -Yspeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RB.velocity = Vector2.zero;
        Anim.Play("SkierFall");
    }
}
