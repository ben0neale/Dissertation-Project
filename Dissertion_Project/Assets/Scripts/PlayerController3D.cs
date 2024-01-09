using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField] GameObject PlayerRagdoll;
    [SerializeField] GameObject PlayerModel;
    [SerializeField] Rigidbody RagdollRB;
    Animator anim;
    Vector3 MoveValue;
    Rigidbody RB;
    public float Xspeed;
    public float XMaxVelocity;
    public float ZMaxVelocity;
    public float rotationSpeed;
    private bool stumbling = false;

    private bool firstHit = false;
    private GameObject objHit;

    private float zSpeed;

    public bool gameOver = false;
    [SerializeField] GameObject HighScoreTable;

    public int multiplier = 0;
    [SerializeField] float multiplierTime;
    float _multiplierTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        zSpeed = ZMaxVelocity;
        RB = GetComponent<Rigidbody>();

        _multiplierTime = multiplierTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            RB.velocity = new Vector3(MoveValue.x * Xspeed, RB.velocity.y, -zSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, MoveValue.x * -45, 0), rotationSpeed);

            if (multiplier > 0)
            { 
                if (_multiplierTime <= 0)
                {
                    multiplier = 0;
                    _multiplierTime = multiplierTime;
                }
                else
                    _multiplierTime -= Time.deltaTime;
            }
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
        zSpeed = ZMaxVelocity - 5;

        yield return new WaitForSeconds(1);

        zSpeed = ZMaxVelocity + 5;

        yield return new WaitForSeconds(1);

        stumbling = false;
        zSpeed = ZMaxVelocity;

    }

    private IEnumerator GameOver()
    {
        PlayerModel.SetActive(false);
        PlayerRagdoll.SetActive(true);
        RB.constraints = RigidbodyConstraints.None;
        RagdollRB.AddForce(0, 2000, -4000);
        gameOver = true;

        yield return new WaitForSeconds(2.5f);

        HighScoreTable.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstical"))
            StartCoroutine(GameOver());
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
                    StartCoroutine(GameOver());
                else
                {
                    objHit = other.gameObject;
                    StartCoroutine(Stumble());
                }
            }
        }
        if (other.gameObject.CompareTag("Multiplier"))
        {
            if (multiplier == 0)
                multiplier = 2;
            else
                multiplier *= 2;
            _multiplierTime = multiplierTime;
        }
    }
}
