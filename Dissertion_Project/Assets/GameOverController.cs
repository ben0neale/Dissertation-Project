using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : PlayerAttributes
{
    [SerializeField] GameObject HighScoreTable;
    [SerializeField] Rigidbody RagdollRB;

    // Update is called once per frame
    public void Update()
    {
        if (transform.position.y < -1 && StateControllerRef.GetComponent<GamestateController>().state == GamestateController.GameState.Play)
            StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        StateControllerRef.GetComponent<GamestateController>().state = GamestateController.GameState.GameOver;
        GetComponent<audioController>().PlayCrash();
        PlayerModel.SetActive(false);
        PlayerRagdoll.SetActive(true);
        RB.constraints = RigidbodyConstraints.None;
        RagdollRB.AddForce(0, 2000, -4000);

        yield return new WaitForSeconds(2.5f);

        //HighScoreTable.SetActive(true);
        continuePanel.SetActive(true);
        StartCoroutine(Close());
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(3f);

        continuePanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Obstical") || collision.gameObject.CompareTag("Skier")) && StateControllerRef.GetComponent<GamestateController>().GetGameState() != GamestateController.GameState.GameOver)
            StartCoroutine(GameOver());


        if (collision.gameObject.CompareTag("Boalder"))
        {
            StartCoroutine(GameOver());
        }
    }


    }
