using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Boalder : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -1 * speed * Time.deltaTime);
    }
}
