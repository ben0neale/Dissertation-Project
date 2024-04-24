using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Camera Cam;
    [SerializeField] int camSize;
    [SerializeField] float camSpeed;
    private bool play = false;
    [SerializeField] GameObject playText;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            play = true;
            playText.SetActive(false);
        }
            
        if(play)
            BeginGame();

        if (Cam.orthographicSize >= camSize -.1f)
            SceneManager.LoadScene(1);
    }

    private void BeginGame()
    {
        Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize,camSize, camSpeed * Time.deltaTime);
    }
}
