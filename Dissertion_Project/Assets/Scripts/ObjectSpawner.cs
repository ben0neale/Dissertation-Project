using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public bool ThreeD = true;

    [SerializeField] List<GameObject> Obsticals;
    [SerializeField] GameObject Skier;
    [SerializeField] GameObject ObjParent;
    [SerializeField] GameObject Player;
    [SerializeField] List<GameObject> Platform;
    [SerializeField] GameObject StartPlat;
    [SerializeField] GameObject Jump;
    [SerializeField] GameObject Boalder;
    GameObject PrevPlat;

    public float x1;
    public float x2;
    public float y1;
    public float y2;

    public float _SkierspawnInterval;
    private float SkierspawnInterval;

    public float _spawnInterval;
    private float spawnInterval;

    public float difficultyCurve;

    public float _jumpSpawnInterval;
    private float jumpSpawnInteral;

    [SerializeField] float platSpawnDistance;
    private float _platSpawnDistance;

    private bool boalderState = false;
    int boalderPlatNum = 0;

    public float _boalderSpawnInterval;
    private float boalderSpawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        spawnInterval = _spawnInterval;
        SkierspawnInterval = _SkierspawnInterval;
        jumpSpawnInteral = _jumpSpawnInterval;
        _platSpawnDistance = 0;
        PrevPlat = StartPlat;
        boalderSpawnInterval = _boalderSpawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        //At given interval spawn an obstical
        if (spawnInterval <= 0 && !boalderState)
        {
            ObsticalSpawn();
            if(_spawnInterval > 0.2)
                _spawnInterval -= difficultyCurve;
            spawnInterval = _spawnInterval;
        }
        else
            spawnInterval -= Time.deltaTime;

/*        if (SkierspawnInterval <= 0)
        {
            SkierSpawn();
            SkierspawnInterval = _SkierspawnInterval;
        }
        else
            SkierspawnInterval -= Time.deltaTime;*/

/*        if (jumpSpawnInteral <= 0)
        {
            JumpSpawner();
            jumpSpawnInteral = _jumpSpawnInterval;
        }
        else
            jumpSpawnInteral -= Time.deltaTime;*/

        if (ThreeD && Player.transform.position.z <= -_platSpawnDistance)
        {
            PlatformSpawn();
            _platSpawnDistance += 80; 
        }

        if(boalderPlatNum == 3)
        {
            boalderState = false;
            boalderPlatNum = 0;
        }

        if(boalderState)
        {
            if (boalderSpawnInterval > 0)
                boalderSpawnInterval -= Time.deltaTime;
            else
            {
                boalderSpawnInterval = _boalderSpawnInterval;
                SpawnBoalder();
            }
        }
    }

    void SpawnBoalder()
    {
        Instantiate(Boalder, new Vector3(Random.Range(x1, x2), 1f, Player.transform.position.z + 20), Quaternion.identity);
    }

    public void ObsticalSpawn()
    {
        //Get random obstical from obstical list
        GameObject obj = Obsticals[Random.Range(0, Obsticals.Count)];

        //Get Random spawn position from provided range
        Vector3 Pos = GetPos(); 
        
        //Instantiate chosen object at chosen position
        GameObject obstical = Instantiate(obj, Pos, Quaternion.Euler(0,Random.Range(0,360),0), ObjParent.transform);

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

    private void JumpSpawner()
    {
        Vector3 pos = GetPos();
        print(pos);
        Instantiate(Jump, pos, Quaternion.identity);
    }

    private Vector3 GetPos()
    {
        if (!ThreeD)
            return new Vector3(Random.Range(x1 + Player.transform.position.x, x2 + Player.transform.position.x), Random.Range(-y1 + Player.transform.position.y, -y2 + Player.transform.position.y), 0);
        else
            return new Vector3(Random.Range(x1, x2), 0, Random.Range(-y1 + Player.transform.position.z, -y2 + Player.transform.position.z));
    }

    private void SkierSpawn()
    {
        Vector3 Pos;
        List<float> spawnSide = new List<float> { -8, 8 };
        float direction = spawnSide[Random.Range(0, 2)]; 
        
        if(!ThreeD)
            Pos = new Vector3(direction + Player.transform.position.x, Random.Range(Player.transform.position.y, Player.transform.position.y - y1), 0);
        else
            Pos = new Vector3(direction + Player.transform.position.x, 1.5f, Random.Range(-y1 + Player.transform.position.z, -y2 + Player.transform.position.z));

        Instantiate(Skier, Pos, Quaternion.identity, ObjParent.transform);
    }

    private void PlatformSpawn()
    {
        GameObject plat;
        if (!boalderState)
        {
            plat = Platform[Random.Range(0, Platform.Count)];
            if (plat == Platform[3])
                boalderState = true;
        }
        else
        {
            plat = Platform[3];
            boalderPlatNum++;
        }
        PrevPlat = Instantiate(plat, new Vector3(-40, -20, PrevPlat.transform.position.z - 80), Quaternion.identity);
    }

}
