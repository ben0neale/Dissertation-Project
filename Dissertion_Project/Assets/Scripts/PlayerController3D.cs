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
    [SerializeField] GameObject Avalanche;
    Animator anim;
    Vector3 MoveValue;
    Rigidbody RB;

    public float Xspeed;
    public float XAcceleration;
    public float XMaxVelocity;
    public float ZMaxVelocity;
    public float rotationSpeed;
    private bool stumbling = false;
    float avalancheOffset;

    private bool firstHit = false;
    private GameObject objHit;

    private float zSpeed;
    private float xValue = 0;

    float xrotation = 0;
    public float MaxRotation;

    public bool gameOver = false;
    [SerializeField] GameObject HighScoreTable;

    bool useGravity = true;
    public float GravScale = -2f;

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
        avalancheOffset = transform.position.z - Avalanche.transform.position.z;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameOver)
        {
            if (transform.position.z - Avalanche.transform.position.z > avalancheOffset && !stumbling)
                zSpeed += 1;
            else if(!stumbling)
                zSpeed = ZMaxVelocity;

            xValue = Mathf.Lerp(xValue, MoveValue.x, XAcceleration * Time.deltaTime);
            xrotation = -xValue;
            RB.velocity = new Vector3(-xrotation * Xspeed, GravScale, -zSpeed);

            //transform.LookAt(transform.position + -RB.velocity - new Vector3(0,1,0));
            transform.rotation = Quaternion.Euler(transform.rotation.x, xrotation * 45, 0);

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
        if ((collision.gameObject.CompareTag("Obstical") || collision.gameObject.CompareTag("Skier")) && !gameOver)
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
