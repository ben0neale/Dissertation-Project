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

    private void Start()
    {
        Player = GameObject.Find("3D player");
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnTime <= 0)
        {
            SpawnTime = _SpawnTime;
            SpawnCoin();
        }
        else
            SpawnTime -= Time.deltaTime;
    }

    void SpawnCoin()
    {
        Instantiate(Coins[Random.Range(0, Coins.Count)], new Vector3(Random.Range(GetComponent<ObjectSpawner>().x1, GetComponent<ObjectSpawner>().x2), .5f, Player.transform.position.z - GetComponent<ObjectSpawner>().y1), Quaternion.identity);
        Instantiate(PowerUps[Random.Range(0, Coins.Count)], new Vector3(Random.Range(GetComponent<ObjectSpawner>().x1, GetComponent<ObjectSpawner>().x2), .5f, Player.transform.position.z - GetComponent<ObjectSpawner>().y1), Quaternion.identity);
    }
}
