using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalSpawner : ObjectSpawner
{
    [SerializeField] List<GameObject> Obsticals;
    [SerializeField] GameObject Boalder;
    [SerializeField] GameObject ObjParent;


    public float _spawnInterval;
    private float spawnInterval;

    public float _boalderSpawnInterval;
    private float boalderSpawnInterval;

    public float _restTime;
    float RestTime;

    public float difficultyCurve;
    public float difficultyChangeInterval;
    private float _difficultyinterval;

    // Start is called before the first frame update
    void Start()
    {
        _difficultyinterval = difficultyChangeInterval;
        boalderSpawnInterval = _boalderSpawnInterval;
        stateControllerRef.SetSpawnState(GamestateController.SpawnState.Obstical);
        RestTime = _restTime;
        spawnInterval = _spawnInterval;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stateControllerRef.spawnState == GamestateController.SpawnState.Obstical || stateControllerRef.spawnState == GamestateController.SpawnState.PreObstical)
        {
            if (spawnInterval <= 0)
            {
                spawnInterval = Random.Range(_spawnInterval * .75f, _spawnInterval * 1.25f);
                //ObsticalSpawn();
            }
            else
                spawnInterval -= Time.deltaTime;

            if(_difficultyinterval <= 0)
            {
                _difficultyinterval = difficultyChangeInterval;
                if (_spawnInterval > 0.5)
                    _spawnInterval -= difficultyCurve;
            }
            else
                _difficultyinterval -= Time.deltaTime;

        }
        else if (stateControllerRef.spawnState == GamestateController.SpawnState.Boalder)
        {
            if (boalderSpawnInterval > 0)
                boalderSpawnInterval -= Time.deltaTime;
            else
            {
                boalderSpawnInterval = _boalderSpawnInterval;
                SpawnBoalder();
            }
        }
        else if (stateControllerRef.spawnState == GamestateController.SpawnState.Rest)
        {
            if (RestTime <= 0)
            {
                stateControllerRef.SetSpawnState(GamestateController.SpawnState.PreObstical);
                RestTime = _restTime;
            }
            else
                RestTime -= Time.deltaTime;
        }
        else if (stateControllerRef.spawnState == GamestateController.SpawnState.PreObstical)
        {
            if (RestTime <= 0)
            {
                stateControllerRef.SetSpawnState(GamestateController.SpawnState.Obstical);
                RestTime = _restTime;
            }
            else
                RestTime -= Time.deltaTime;
        }
    }

    void SpawnBoalder()
    {
        Instantiate(Boalder, new Vector3(Random.Range(x1 * 1.75f, -x1 * 1.75f), 1f, Player.transform.position.z + 20), Quaternion.identity);
    }

    public void ObsticalSpawn()
    {
        //Get random obstical from obstical list
        GameObject obj = Obsticals[Random.Range(0, Obsticals.Count)];

        //Get Random spawn position from provided range
        Vector3 Pos = GetPos();

        //Instantiate chosen object at chosen position
        GameObject obstical = Instantiate(obj, Pos, Quaternion.Euler(0, Random.Range(0, 360), 0), ObjParent.transform);
    }

    private Vector3 GetPos()
    {
 //       if (!ThreeD)
          return new Vector3(Random.Range(x1, -x1), 1, Random.Range(-y1 + Player.transform.position.z, -y1 + Player.transform.position.z));
//        else
//            return new Vector3(Random.Range(x1, x2), 0, Random.Range(-y1 + Player.transform.position.z, -y2 + Player.transform.position.z));
    }

/*    private void JumpSpawner()
    {
        Vector3 pos = GetPos();
        print(pos);
        Instantiate(Jump, pos, Quaternion.identity);
    }



    private void SkierSpawn()
    {
        Vector3 Pos;
        List<float> spawnSide = new List<float> { -8, 8 };
        float direction = spawnSide[Random.Range(0, 2)];

        if (!ThreeD)
            Pos = new Vector3(direction + Player.transform.position.x, Random.Range(Player.transform.position.y, Player.transform.position.y - y1), 0);
        else
            Pos = new Vector3(direction + Player.transform.position.x, 1.5f, Random.Range(-y1 + Player.transform.position.z, -y2 + Player.transform.position.z));

        Instantiate(Skier, Pos, Quaternion.identity, ObjParent.transform);
    }*/
}
