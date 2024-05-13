using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [SerializeField] GamestateController stateController;
    GameObject Player;
    Rigidbody RB;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        offset = Player.transform.position - transform.position;
        RB = GetComponent<Rigidbody>();       
    }

    private void Update()
    {
        if (stateController.GetComponent<GamestateController>().state == GamestateController.GameState.Play)
            transform.position = new Vector3(0, transform.position.y, Player.transform.position.z - offset.z);
        else
            RB.velocity = Vector3.zero;
    }
}
