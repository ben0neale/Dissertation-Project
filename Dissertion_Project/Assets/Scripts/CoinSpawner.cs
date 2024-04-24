using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> Coins;
    [SerializeField] List<GameObject> PowerUps;
    GameObject Player;
    public float _SpawnTime;
    private float SpawnTime;

    public float _powerupSpawnTime;
    private float powerupSpawnTime;

    private void Start()
    {
        Player = GameObject.Find("3D player");
        powerupSpawnTime = _powerupSpawnTime;
        SpawnTime = _SpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnTime <= 0)
        {
            if(GetComponent<ObjectSpawner>().state == ObjectSpawner.State.rest)
                SpawnTime = _SpawnTime * .25f;
            else
                SpawnTime = Random.Range(_SpawnTime * .75f, _SpawnTime * 1.25f);
            SpawnCoin();
        }
        else
            SpawnTime -= Time.deltaTime;

        if(powerupSpawnTime <= 0)
        {
            powerupSpawnTime = Random.Range(_powerupSpawnTime * .75f, _powerupSpawnTime * 1.25f);
            SpawnPowerup();
        }
        else
            powerupSpawnTime -= Time.deltaTime;
    }

    void SpawnCoin()
    {
        Instantiate(Coins[Random.Range(0, Coins.Count)], new Vector3(Random.Range(GetComponent<ObjectSpawner>().x1, -GetComponent<ObjectSpawner>().x1), .5f,Random.Range(Player.transform.position.z - GetComponent<ObjectSpawner>().y1, Player.transform.position.z - GetComponent<ObjectSpawner>().y1 - 4)), Quaternion.identity);
    }

    void SpawnPowerup()
    {
        Instantiate(PowerUps[Random.Range(0, PowerUps.Count)], new Vector3(Random.Range(GetComponent<ObjectSpawner>().x1, -GetComponent<ObjectSpawner>().x1), .5f, Random.Range(Player.transform.position.z - GetComponent<ObjectSpawner>().y1, Player.transform.position.z - GetComponent<ObjectSpawner>().y1 - 4)), Quaternion.identity);
    }
}
