using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    GameObject Spawner;
    public bool randomScale = true;
    private bool respawning = false;

    // Start is called before the first frame update
    void Start()
    {
        Spawner = GameObject.FindGameObjectWithTag("Spawner");

        if(randomScale)
            transform.localScale = Vector3.one * Random.Range(.5f, 1.5f);
    }

    private void Update()
    {
        if (transform.position.y < -1 & !respawning)
        {
            Respawn();
        }

    }

    void Respawn()
    {
        respawning = true;
        //Spawner.GetComponent<ObsticalSpawner>().ObsticalSpawn();
        Destroy(gameObject);
    }

/*    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstical"))
        {
            if (!respawning)
            {
                //Destroy(other.gameObject);
                Respawn();
            }
        }
    }*/

/*    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstical"))
        {
            //Spawner.GetComponent<ObjectSpawner>().ObsticalSpawn();
            if (transform.position.z < collision.gameObject.transform.position.z)
            {
                Respawn();
            }
      */  //}
   // }
}
