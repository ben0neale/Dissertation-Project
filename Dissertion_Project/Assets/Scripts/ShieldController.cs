using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] float _shieldTime;
    bool flahing = false;
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
                flahing = false;
                gameObject.SetActive(false);
            }
            else
                shieldTime -= Time.deltaTime;
        }
        else
            ResetTime();

        if (shieldTime <= 1.2f && !flahing)
        {
            flahing = true;
            StartCoroutine(Flash());
        }

    }


    IEnumerator Flash()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(.3f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(.3f);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(.3f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void ResetTime()
    {
        shieldTime = _shieldTime;
        flahing = false;
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
