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

        if(stateControllerRef.spawnState == GamestateController.SpawnState.Boalder)
        {
            if (boalderPlatNum >= 2)
            {
                stateControllerRef.SetSpawnState(GamestateController.SpawnState.Rest);
                boalderPlatNum = 0;
            }
        }
    }

    private void PlatformSpawn()
    {
        GameObject plat;
        if (stateControllerRef.spawnState == GamestateController.SpawnState.Obstical || stateControllerRef.spawnState == GamestateController.SpawnState.PreObstical)
        {
            if (Player.transform.position.z > -300)
            {
                if(PrevPlat == Platform[3])
                    plat = Platform[Random.Range(0, Platform.Count - 1)];
                else
                    plat = Platform[Random.Range(0, Platform.Count)];
            }

            else
            {
                if (PrevPlat == Platform[3])
                    plat = StageTwoPlatforms[Random.Range(0, Platform.Count - 1)];
                else
                    plat = Platform[Random.Range(0, Platform.Count)];
            }
                
/*            if (plat == Platform[3] || plat == StageTwoPlatforms[3])
                stateControllerRef.SetSpawnState(GamestateController.SpawnState.Boalder);*/
        }
        else if (stateControllerRef.spawnState == GamestateController.SpawnState.Boalder)
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
        if (plat == Platform[3] && stateControllerRef.spawnState == GamestateController.SpawnState.Obstical)
            stateControllerRef.spawnState = GamestateController.SpawnState.PreBoulder;
    }
}
