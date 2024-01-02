using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Vector3 Offset;

    private void Start()
    {
        Offset = Player.transform.position - transform.position;
    }

    private void Update()
    {
        transform.position = Player.transform.position - Offset;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
