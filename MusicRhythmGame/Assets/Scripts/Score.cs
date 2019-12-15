using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0.0f;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;

    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        if (score >= scoreToNextLevel) {
            LevelUp();
        }
        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp() {
        // game ends when the player reaches max diffuclty level
        if (difficultyLevel == maxDifficultyLevel) {
            return;
        }
        // Threshold to move to next level is doubled
        scoreToNextLevel *= 2;
        difficultyLevel++;

        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);

        Debug.Log(difficultyLevel);
    }
}
