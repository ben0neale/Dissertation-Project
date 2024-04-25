using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Boalder : MonoBehaviour
{
   // public float speed;
    GameObject Player;
    private float speed;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        speed = Random.Range(2.25f, 3.25f);
    }

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1 * Player.GetComponent<PlayerAttributes>().ZMaxVelocity * speed * Time.deltaTime);
    }
}
