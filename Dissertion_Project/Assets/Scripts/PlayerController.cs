using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Vector2 MoveValue;
    Rigidbody2D RB;
    public float Xspeed;
    public float Yspeed;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(MoveValue.x * Xspeed, -Yspeed);
    }

    void OnMove(InputValue MoveInput)
    {
        MoveValue = MoveInput.Get<Vector2>();
        //RB.velocity = RB.velocity + MoveValue * Xspeed;
        transform.rotation = Quaternion.Euler(0,0, -MoveValue.x * 15);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Temp Respawn
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
