using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public float scoreValue = 0, scorePerSecond;
    public Text score;
    
    void Start() //used for initialization
    {
       score = GetComponent<Text>();
       scorePerSecond = 1;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + (int)scoreValue;
        scoreValue += scorePerSecond * Time.deltaTime;
    }

    public void UpdateScore(float increase)
    {
        scoreValue = increase + scoreValue;
    }
}
