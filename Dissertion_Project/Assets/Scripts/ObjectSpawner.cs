using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public bool ThreeD = true;

    [SerializeField] List<GameObject> Obsticals;
    [SerializeField] GameObject Skier;
    [SerializeField] GameObject ObjParent;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Platform;
    [SerializeField] GameObject StartPlat;
    [SerializeField] GameObject Jump;
    GameObject PrevPlat;

    public float x1;
    public float x2;
    public float y1;
    public float y2;

    public float _SkierspawnInterval;
    private float SkierspawnInterval;

    public float _spawnInterval;
    private float spawnInterval;

    [SerializeField] float platSpawnDistance;
    private float _platSpawnDistance;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        spawnInterval = _spawnInterval;
        SkierspawnInterval = _SkierspawnInterval;
        _platSpawnDistance = Platform.transform.localScale.z / 2;
        PrevPlat = StartPlat;
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

        if (SkierspawnInterval <= 0)
        {
            SkierSpawn();
            SkierspawnInterval = _SkierspawnInterval;
        }
        else
            SkierspawnInterval -= Time.deltaTime;

        if (ThreeD && Player.transform.position.z <= -_platSpawnDistance)
        {
            PlatformSpawn();
            _platSpawnDistance += Platform.transform.localScale.z; 
        }
    }

    private void ObsticalSpawn()
    {
        //Get random obstical from obstical list
        GameObject obj = Obsticals[Random.Range(0, Obsticals.Count)];

        //Get Random spawn position from provided range
        Vector3 Pos;
        if (!ThreeD)
            Pos = new Vector3(Random.Range(x1 + Player.transform.position.x, x2 + Player.transform.position.x), Random.Range(-y1 + Player.transform.position.y, -y2 + Player.transform.position.y), 0);
        else
            Pos = new Vector3(Random.Range(x1 + Player.transform.position.x, x2 + Player.transform.position.x), .5f, Random.Range(-y1 + Player.transform.position.z, -y2 + Player.transform.position.z));
        
        //Instantiate chosen object at chosen position
        GameObject obstical = Instantiate(obj, Pos, Quaternion.Euler(0,Random.Range(0,360),0), ObjParent.transform);
        if (obstical.CompareTag("Multiplier"))
            obstical.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (!ThreeD)
        {
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

    private void SkierSpawn()
    {
        Vector3 Pos;
        List<float> spawnSide = new List<float> { x1, x2 };
        float direction = spawnSide[Random.Range(0, 2)]; 
        
        if(!ThreeD)
            Pos = new Vector3(direction + Player.transform.position.x, Random.Range(Player.transform.position.y, Player.transform.position.y - y1), 0);
        else
            Pos = new Vector3(direction + Player.transform.position.x, 1.5f, Random.Range(Player.transform.position.z, Player.transform.position.z - y1));

        Instantiate(Skier, Pos, Quaternion.identity, ObjParent.transform);
    }

    private void PlatformSpawn()
    {
        PrevPlat = Instantiate(Platform, new Vector3(Player.transform.position.x, 0, PrevPlat.transform.position.z - Platform.transform.localScale.z), Quaternion.identity);
    }
}
