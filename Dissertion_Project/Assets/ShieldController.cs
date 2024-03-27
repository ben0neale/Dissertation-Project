using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] float _shieldTime;
    private float shieldTime;

    private void Start()
    {
        shieldTime = _shieldTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (shieldTime <= 0)
            {
                shieldTime = _shieldTime;
                gameObject.SetActive(false);
            }
            else
                shieldTime -= Time.deltaTime;
        }
        else
            shieldTime = _shieldTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstical"))
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }

}
