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
    
    private bool isDead = false;

    public Text scoreText;
    public DeathMenu deathMenu;

    // Update is called once per frame
    void Update()
    {
        if (isDead) {
            return;
        }

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

    public void OnDeath() {
        isDead = true;
        if (PlayerPrefs.GetFloat("Highscore") < score) {
            PlayerPrefs.SetFloat("Highscore", score); // Set the highest score into Registry
        }
        deathMenu.ToggleEndMenu(score);
    }
}
