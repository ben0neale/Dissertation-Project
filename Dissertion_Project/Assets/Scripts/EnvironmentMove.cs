using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMove : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
