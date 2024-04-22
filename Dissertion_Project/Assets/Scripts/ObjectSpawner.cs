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

    public enum State
    {
        obstical,
        boalder,
        rest
    }

    public State state = State.obstical;

    int boalderPlatNum = 0;

    public float _boalderSpawnInterval;
    private float boalderSpawnInterval;

    public float _restTime;
    float RestTime;

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
        state = State.obstical;
        RestTime = _restTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.obstical)
        {
            if (spawnInterval <= 0)
            {
                ObsticalSpawn();
                if (_spawnInterval > 0.5)
                    _spawnInterval -= difficultyCurve;
                spawnInterval = Random.Range(_spawnInterval * .75f, _spawnInterval * 1.25f);
            }
            else
                spawnInterval -= Time.deltaTime;
        }
        else if (state == State.boalder)
        {
            if (boalderSpawnInterval > 0)
                boalderSpawnInterval -= Time.deltaTime;
            else
            {
                boalderSpawnInterval = _boalderSpawnInterval;
                SpawnBoalder();
            }

            if (boalderPlatNum == 1)
            {
                state = State.rest;
                boalderPlatNum = 0;
            }
        }
        else if (state == State.rest)
        {
            if (RestTime <= 0)
            {
                state = State.obstical;
                RestTime = _restTime;
            }
            else
                RestTime -= Time.deltaTime;
        }

        if (ThreeD && Player.transform.position.z <= -_platSpawnDistance)
        {
            PlatformSpawn();
            _platSpawnDistance += 80;
        }



        //At given interval spawn an obstical


        /*        if (SkierspawnInterval <= 0)
                {
                    SkierSpawn();
                    SkierspawnInterval = _SkierspawnInterval;
                }
                else
                    SkierspawnInterval -= Time.deltaTime;*/

    }

    void SpawnBoalder()
    {
        Instantiate(Boalder, new Vector3(Random.Range(x1 * 2, x2 * 2), 1f, Player.transform.position.z + 20), Quaternion.identity);
    }

    public void ObsticalSpawn()
    {
        //Get random obstical from obstical list
        GameObject obj = Obsticals[Random.Range(0, Obsticals.Count)];

        //Get Random spawn position from provided range
        Vector3 Pos = GetPos(); 
        
        //Instantiate chosen object at chosen position
        GameObject obstical = Instantiate(obj, Pos, Quaternion.Euler(0,Random.Range(0,360),0), ObjParent.transform);
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
        if (state == State.obstical)
        {
            plat = Platform[Random.Range(0, Platform.Count)];
            if (plat == Platform[3])
                state = State.boalder;
        }
        else if (state == State.boalder)
        {
            plat = Platform[3];
            boalderPlatNum++;
        }
        else
            plat = Platform[3];
        PrevPlat = Instantiate(plat, new Vector3(-40, -20, PrevPlat.transform.position.z - 80), Quaternion.identity);
    }

}
