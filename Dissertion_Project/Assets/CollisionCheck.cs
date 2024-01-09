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
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstical") || other.gameObject.CompareTag("Multiplier"))
        {
            Spawner.GetComponent<ObjectSpawner>().ObsticalSpawn();
            Destroy(gameObject);
        }
    }
}
