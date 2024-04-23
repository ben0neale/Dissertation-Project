using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Boalder : MonoBehaviour
{
   // public float speed;
    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1 * Player.GetComponent<PlayerAttributes>().ZMaxVelocity * 1.25f * Time.deltaTime);
    }
}
