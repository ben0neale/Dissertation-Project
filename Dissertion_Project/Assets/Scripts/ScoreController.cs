using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    ScriptReferencer referencer;

    GameObject Player;
    [SerializeField] GameObject highScoreTable;
    TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI multiplierText;
    public GameObject StateControllerRef;

    public float UpdateTime;
    private float _updateTime;
    int score = 0;

    // Start is called before the first frame update
    public void Start()
    {
        referencer = GameObject.Find("ScriptReferencer").GetComponent<ScriptReferencer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        score = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
        _updateTime = UpdateTime;
    }

    // Update is called once per frame
    public void Update()
    {
        if (StateControllerRef.GetComponent<GamestateController>().GetGameState() == GamestateController.GameState.Play)
        {
            if (_updateTime <= 0)
            {
                score += 1 + Player.GetComponent<ItemCollection>().multiplier;
                _updateTime = UpdateTime;
            }
            else
                _updateTime -= Time.deltaTime;
        }

        multiplierText.text = Player.GetComponent<ItemCollection>().multiplier.ToString() + "x";
        scoreText.text = score.ToString();
    }


    public void UpdateLeaderboard(string name)
    {
        highScoreTable.GetComponent<HighScoreTable>().AddHighscoreEntry(score, name);
        highScoreTable.GetComponent<HighScoreTable>().LoadHighScoreTable();
    }
}
