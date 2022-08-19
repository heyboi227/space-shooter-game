using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text highScoreText;
    [SerializeField]
    private TMP_Text gameOverText;
    [SerializeField]
    private TMP_Text restartLevelInstructionsText;
    [SerializeField]
    private Image lifeImage;
    [SerializeField]
    private Sprite[] lifeSprites;

    private int score = 0;
    private int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
        gameOverText.enabled = false;
        restartLevelInstructionsText.enabled = false;
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void CheckHighScore()
    {
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High score: " + highScore;
        }
    }

    public void UpdateLives(int currentLives)
    {
        lifeImage.sprite = lifeSprites[currentLives];
        if (currentLives <= 0)
        {
            DoGameOver();
        }
    }

    private void DoGameOver()
    {
        restartLevelInstructionsText.enabled = true;
        StartCoroutine(FlickerGameOver());
        CheckHighScore();
    }

    IEnumerator FlickerGameOver()
    {
        gameOverText.enabled = true;
        while (true)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "You Lose";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
