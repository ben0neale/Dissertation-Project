using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D RB;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue MoveInput)
    {
        Vector2 MoveValue = MoveInput.Get<Vector2>();
        RB.velocity = MoveValue * speed;
        transform.rotation = Quaternion.Euler(0,0, -MoveValue.x * 15);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Temp Respawn
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
