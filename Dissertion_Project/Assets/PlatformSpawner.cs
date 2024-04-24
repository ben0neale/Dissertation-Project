using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : ObjectSpawner
{
    [SerializeField] List<GameObject> Platform;
    [SerializeField] List<GameObject> StageTwoPlatforms;
    [SerializeField] GameObject StartPlat;
    GameObject PrevPlat;

    [SerializeField] float platSpawnDistance;
    private float _platSpawnDistance;

    public int boalderPlatNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        _platSpawnDistance = platSpawnDistance;
        PrevPlat = StartPlat;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z <= -_platSpawnDistance)
        {
            PlatformSpawn();
            _platSpawnDistance += 80;
        }

        if(state == State.boalder)
        {
            if (boalderPlatNum == 2)
            {
                state = State.rest;
                boalderPlatNum = 0;
            }
        }
    }

    private void PlatformSpawn()
    {
        GameObject plat;
        if (state == State.obstical)
        {
            if (Player.transform.position.z > -300)
                plat = Platform[Random.Range(0, Platform.Count)];
            else
                plat = StageTwoPlatforms[Random.Range(0, Platform.Count)];
            if (plat == Platform[3] || plat == StageTwoPlatforms[3])
                state = State.boalder;
        }
        else if (state == State.boalder)
        {
            if (Player.transform.position.z > -300)
                plat = Platform[3];
            else
                plat = StageTwoPlatforms[3];
            boalderPlatNum++;
        }
        else
        {
            if (Player.transform.position.z > -300)
                plat = Platform[3];
            else
                plat = StageTwoPlatforms[3];
        }

        PrevPlat = Instantiate(plat, new Vector3(-40, -20, PrevPlat.transform.position.z - 80), Quaternion.identity);
    }
}
