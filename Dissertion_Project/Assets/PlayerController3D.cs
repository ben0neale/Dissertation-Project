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
    public float rotationSpeed;

    private float zSpeed;

    // Start is called before the first frame update
    void Start()
    {
        zSpeed = ZMaxVelocity;
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

        RB.velocity = new Vector3(MoveValue.x * Xspeed, 0, -zSpeed);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, MoveValue.x * -45, 0), rotationSpeed);
    }

    void OnMove(InputValue MoveInput)
    {
        MoveValue = MoveInput.Get<Vector2>();
        // transform.rotation = Quaternion.Euler(0,0, -MoveValue.x * 15);
    }

    IEnumerator Stumble()
    {
        zSpeed = ZMaxVelocity / 2;

        yield return new WaitForSeconds(2);

        zSpeed = ZMaxVelocity * 2;

        yield return new WaitForSeconds(2);

        zSpeed = ZMaxVelocity;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstical"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstical"))
            StartCoroutine(Stumble());
    }
}
