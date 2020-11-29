using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highscore;
    public int highScoreValue;
    public ScoreScript actScore;
    public int score;

    void Start()
    {
        score = (int)actScore.scoreValue;
        highscore.text ="High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void Update()
    {
        score = (int)actScore.scoreValue;
        if(score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreValue = score;
            highscore.text = "High Score: " + highScoreValue;
        }
    }
}
