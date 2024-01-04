using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController3D : MonoBehaviour
{
    Vector3 MoveValue;
    Rigidbody RB;
    public float Xspeed;
    public float XMaxVelocity;
    public float ZMaxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(RB.velocity);

        /*        if (RB.velocity.z > -ZMaxVelocity)
                    RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, -ZMaxVelocity);
                if (RB.velocity.y > -ZMaxVelocity)
                    RB.velocity = new Vector3(RB.velocity.x, ZMaxVelocity, RB.velocity.z);*/
        if (RB.velocity.magnitude >= ZMaxVelocity)
            RB.velocity = new Vector3(MoveValue.x * Xspeed, -ZMaxVelocity, -ZMaxVelocity);
        else
            RB.velocity = new Vector3(MoveValue.x * Xspeed, RB.velocity.y, RB.velocity.z);
    }

    void OnMove(InputValue MoveInput)
    {
        MoveValue = MoveInput.Get<Vector2>();
        // transform.rotation = Quaternion.Euler(0,0, -MoveValue.x * 15);
    }
}
