using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] GamestateController stateController;
    GameObject Player;
    Rigidbody RB;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody>();       
    }

    private void FixedUpdate()
    {
        if (stateController.GetComponent<GamestateController>().state == GamestateController.GameState.Play)
            RB.velocity = new Vector3(0, 0, -Player.GetComponent<PlayerMovement>().ZMaxVelocity * Time.deltaTime);
        else
            RB.velocity = Vector3.zero;
    }
}
