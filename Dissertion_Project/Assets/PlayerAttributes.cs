using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public ScriptReferencer referencer;
    public Rigidbody RB;
    public Animator anim;
    public GameObject StateControllerRef;

    public GameObject PlayerRagdoll, PlayerModel;

    public GameObject continuePanel;

    public float ZMaxVelocity;
    public float zSpeed;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        referencer = GameObject.Find("ScriptReferencer").GetComponent<ScriptReferencer>();
        anim = GetComponentInChildren<Animator>();
    }
}
