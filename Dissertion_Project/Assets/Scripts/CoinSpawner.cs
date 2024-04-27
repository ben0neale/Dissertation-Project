using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : ObjectSpawner
{
    [SerializeField] List<GameObject> Coins;
    [SerializeField] List<GameObject> RestCoins;
    [SerializeField] List<GameObject> PowerUps;
    public float _SpawnTime;
    private float SpawnTime;

    public float _powerupSpawnTime;
    private float powerupSpawnTime;
    bool restSpawned = false;

    private void Start()
    {
        Player = GameObject.Find("3D player");
        powerupSpawnTime = _powerupSpawnTime;
        SpawnTime = _SpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (stateControllerRef.spawnState == GamestateController.SpawnState.Rest && !restSpawned)
        {
            restSpawned = true;
            //Instantiate(RestCoins[0], new Vector3(0, .5f, Random.Range(Player.transform.position.z - y1, Player.transform.position.z - y1 - 4)), Quaternion.identity);
        }          
        else
        {
            if (SpawnTime <= 0)
            {
                SpawnTime = Random.Range(_SpawnTime * .75f, _SpawnTime* 1.25f);
                SpawnCoin();
            }
            else
                SpawnTime -= Time.deltaTime;

            if (powerupSpawnTime <= 0)
            {
                powerupSpawnTime = Random.Range(_powerupSpawnTime * .75f, _powerupSpawnTime * 1.25f);
                SpawnPowerup();
            }
            else
                powerupSpawnTime -= Time.deltaTime;
        }

/*        if (stateControllerRef.spawnState != GamestateController.SpawnState.Rest && restSpawned)
            restSpawned = false;*/
    }

    void SpawnCoin()
    {
        Instantiate(Coins[Random.Range(0, Coins.Count)], new Vector3(Random.Range(x1, -x1), .5f,Random.Range(Player.transform.position.z - y1, Player.transform.position.z - y1 - 4)), Quaternion.identity);
    }

    void SpawnPowerup()
    {
        Instantiate(PowerUps[Random.Range(0, PowerUps.Count)], new Vector3(Random.Range(x1, -x1), .5f, Random.Range(Player.transform.position.z - y1, Player.transform.position.z - y1 - 4)), Quaternion.identity);
    }
}
