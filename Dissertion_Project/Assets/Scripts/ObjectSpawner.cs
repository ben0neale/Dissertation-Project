using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //[SerializeField] GameObject Skier;
    public GameObject Player;
    public GamestateController stateControllerRef;
    //[SerializeField] GameObject Jump;

    /*    public float _SkierspawnInterval;
        private float SkierspawnInterval;*/

    /*    public float _jumpSpawnInterval;
        private float jumpSpawnInteral;*/

    public float x1;
    public float y1;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

/*        SkierspawnInterval = _SkierspawnInterval;
        jumpSpawnInteral = _jumpSpawnInterval;*/
    }

    // Update is called once per frame
    void Update()
    {
        //At given interval spawn an obstical


        /*        if (SkierspawnInterval <= 0)
                {
                    SkierSpawn();
                    SkierspawnInterval = _SkierspawnInterval;
                }
                else
                    SkierspawnInterval -= Time.deltaTime;*/

    }
}
