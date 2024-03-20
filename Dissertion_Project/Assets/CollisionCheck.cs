using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    GameObject Spawner;


    // Start is called before the first frame update
    void Start()
    {
        Spawner = GameObject.FindGameObjectWithTag("Spawner");

        transform.localScale = Vector3.one * Random.Range(.3f, 1);
    }

    private void Update()
    {
        if (transform.position.y < -3)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstical"))
        {
            //Spawner.GetComponent<ObjectSpawner>().ObsticalSpawn();
            if (transform.position.z < other.gameObject.transform.position.z)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstical"))
        {
            //Spawner.GetComponent<ObjectSpawner>().ObsticalSpawn();
            if (transform.position.z < collision.gameObject.transform.position.z)
            {
                Destroy(gameObject);
            }
        }
    }
}
