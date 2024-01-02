using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> Obsticals;
    [SerializeField] GameObject ObjParent;
    public float x1;
    public float x2;
    public float y1;
    public float y2;

    public float _spawnInterval;
    private float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = _spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        //At given interval spawn an obstical
        if (spawnInterval <= 0)
        {
            ObsticalSpawn();
            spawnInterval = _spawnInterval;
        }
        else
            spawnInterval -= Time.deltaTime;
    }

    private void ObsticalSpawn()
    {
        //Get random obstical from obstical list
        GameObject obj = Obsticals[Random.Range(0, Obsticals.Count)];
        //Get Random spawn position from provided range
        Vector3 Pos = new Vector3(Random.Range(x1, x2), Random.Range(y1, y2), 0);

        //Instantiate chosen object at chosen position
        GameObject obstical = Instantiate(obj, Pos, Quaternion.identity, ObjParent.transform);

        //Create results array and contact filter for the OverlapCollider funtion
        Collider2D[] results = new Collider2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D();
        
        //If the tree overlaps with any other obstical, destroy it and spawn another (Stops two objects spawning on top of eachother
        if (obstical.GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, results) > 0)
        {
            Destroy(obstical);
            ObsticalSpawn();
        }

    }
}
