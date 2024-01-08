using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] GameObject highScoreTable;
    TextMeshProUGUI scoreText;
    public float UpdateTime;
    private float _updateTime;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        _updateTime = UpdateTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_updateTime <= 0)
        {
            score++;
            _updateTime = UpdateTime;
        }
        else
            _updateTime -= Time.deltaTime;

        scoreText.text = score.ToString();
    }

    public void UpdateLeaderboard(string name)
    {
        highScoreTable.GetComponent<HighScoreTable>().AddHighscoreEntry(score, name);
        highScoreTable.GetComponent<HighScoreTable>().LoadHighScoreTable();
    }
}
