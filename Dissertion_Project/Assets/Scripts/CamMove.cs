using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    GameObject Player;
    Rigidbody RB;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody>();       
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector3(0, 0, -Player.GetComponent<PlayerController3D>().ZMaxVelocity);
    }
}
