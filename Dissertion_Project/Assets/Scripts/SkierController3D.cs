using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkierController3D : MonoBehaviour
{
    [SerializeField] GameObject Ragdol;
    [SerializeField] GameObject Model;
    [SerializeField] Rigidbody RagdolRB;
    GameObject Player;
    Rigidbody RB;
    public float minSpeed, maxSpeed;
    public float Yspeed;
    int direction;
    bool crash = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody>();

        if (Player.transform.position.x < transform.position.x)
            direction = -1;
        else
            direction = 1;

        RB.velocity = new Vector3(Random.Range(minSpeed, maxSpeed) * direction, RB.velocity.y, Random.Range(-Yspeed - 5, -Yspeed + 5));
        transform.LookAt(RB.velocity * 1000);
    }

    private void FixedUpdate()
    {
        if (!crash)
        {
            RB.velocity = new Vector3(Random.Range(minSpeed, maxSpeed) * direction, RB.velocity.y, Random.Range(-Yspeed - 5, -Yspeed + 5));

            if (transform.position.y < -3)
                death();
        }
    }

    void death()
    {
        crash = true;
        RB.velocity = Vector3.zero;
        RB.constraints = RigidbodyConstraints.None;
        Model.SetActive(false);
        Ragdol.SetActive(true);
        RagdolRB.AddForce(0, 300, 300);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstical") || collision.gameObject.CompareTag("Player"))
        {
            death();
        }
    }
}
