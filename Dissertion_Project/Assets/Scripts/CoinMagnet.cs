using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    GameObject Player;
    public float MagnetRange;
    public float speed;
    private bool follow = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) < MagnetRange)
            follow = true;

        if(follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            speed += .1f;
        }
    }
}
