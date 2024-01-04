using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkierController3D : MonoBehaviour
{
    GameObject Player;
    Rigidbody RB;
    public float minSpeed, maxSpeed;
    public float Yspeed;
    int direction;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody>();

        if (Player.transform.position.x < transform.position.x)
            direction = -1;
        else
            direction = 1;

        RB.velocity = new Vector3(Random.Range(minSpeed, maxSpeed) * direction, 0, -Yspeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            RB.velocity = Vector3.zero;
            RB.constraints = RigidbodyConstraints.None;
        }
            
    }
}
