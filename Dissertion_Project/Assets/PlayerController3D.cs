using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        RB.velocity = new Vector3(MoveValue.x * Xspeed, 0, -ZMaxVelocity);
    }

    void OnMove(InputValue MoveInput)
    {
        MoveValue = MoveInput.Get<Vector2>();
        // transform.rotation = Quaternion.Euler(0,0, -MoveValue.x * 15);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstical"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
