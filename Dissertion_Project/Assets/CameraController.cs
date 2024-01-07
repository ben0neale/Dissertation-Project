using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] bool followX = true;
    [SerializeField] bool followY = true;
    GameObject Player;
    Vector3 Offset;
    public float camSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Offset = Player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Target;
        if (followX && followY)
            Target = new Vector3(Player.transform.position.x - Offset.x, Player.transform.position.y - Offset.y, Player.transform.position.z - Offset.z);
        else if (followX)
            Target = new Vector3(Player.transform.position.x - Offset.x, transform.position.y, transform.position.z);
        else if (followY)
            Target = new Vector3(transform.position.x, Player.transform.position.y - Offset.y, Player.transform.position.z - Offset.z);
        else
            Target = transform.position;
        transform.position = Vector3.Lerp(transform.position, Target, camSpeed);
    }
}
