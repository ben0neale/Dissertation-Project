using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerAttributes
{
    public GameObject AvalancheRef;

    private bool firstHit = false;
    private GameObject objHit;

    [SerializeField] float Xspeed, XAccelerationRotation, ZpeedIncreaseRate, speedIncreaseTime;
    private float xValue = 0, xrotation = 0, avalancheOffset, _speedIncreaseTime;

    private bool stumbling = false;

    Vector3 MoveValue;

    // Start is called before the first frame update
    public void Start()
    {
        zSpeed = ZMaxVelocity;
        speedIncreaseTime = _speedIncreaseTime;
        avalancheOffset = transform.position.z - AvalancheRef.transform.position.z;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if(StateControllerRef.GetComponent<GamestateController>().GetGameState() == GamestateController.GameState.Play)
        {
            ApplyMovement();
            IncreaseSpeed();
        }
    }

    private void ApplyMovement()
    {
/*        if (transform.position.z - AvalancheRef.transform.position.z > avalancheOffset && !stumbling)
            zSpeed += .3F;
        else if (!stumbling)*/
        zSpeed = ZMaxVelocity;

        xValue = Mathf.Lerp(xValue, MoveValue.x, XAccelerationRotation * Time.deltaTime);
        xrotation = -xValue;
        RB.velocity = new Vector3(-xrotation * Xspeed, -100, -zSpeed) * Time.deltaTime;

        //transform.LookAt(transform.position + -RB.velocity - new Vector3(0,1,0));
        transform.rotation = Quaternion.Euler(transform.rotation.x, xrotation * 45, 0);
    }

    private void IncreaseSpeed()
    {
        if (speedIncreaseTime >= 0)
            speedIncreaseTime -= Time.deltaTime;
        else
        {
            ZMaxVelocity += ZpeedIncreaseRate;
            speedIncreaseTime = _speedIncreaseTime;
        }
    }

    void OnMove(InputValue MoveInput)
    {
        MoveValue = MoveInput.Get<Vector2>();
        // transform.rotation = Quaternion.Euler(0,0, -MoveValue.x * 15);
    }

    IEnumerator Stumble()
    {
        anim.Play("PlayerStumble3D");
        stumbling = true;
       // zSpeed = ZMaxVelocity - 5;

        yield return new WaitForSeconds(1);

        //zSpeed = ZMaxVelocity + 5;

        yield return new WaitForSeconds(1);

        stumbling = false;
        zSpeed = ZMaxVelocity;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstical") || other.gameObject.CompareTag("Skier"))
        {
            if (!firstHit)
            {
                firstHit = true;
                objHit = other.gameObject;
            }

            if (other.gameObject != objHit)
            {
                if (stumbling)
                    print("GAME OVER");
                //StartCoroutine(GameOver());
                else
                {
                    objHit = other.gameObject;
                    StartCoroutine(Stumble());
                }
            }
        }

        if((other.gameObject.name == "WideTerrain(Clone)" || other.gameObject.name == "WideTerrainGrassy(Clone)") && StateControllerRef.GetComponent<GamestateController>().spawnState == GamestateController.SpawnState.PreBoulder)
        {
            StateControllerRef.GetComponent<GamestateController>().SetSpawnState(GamestateController.SpawnState.Boalder);
        }
    }
}
