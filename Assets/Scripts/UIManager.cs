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
    private Image redLifeImage;
    [SerializeField]
    private Sprite[] redLifeSprites;
    [SerializeField]
    private Image blueLifeImage;
    [SerializeField]
    private Sprite[] blueLifeSprites;

    private GameManager gameManager;
    private SpawnManager spawnManager;

    private int score = 0;
    private int highScore = 0;

    private bool isPlayerOneDead = false;
    private bool isPlayerTwoDead = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
        gameOverText.enabled = false;
        restartLevelInstructionsText.enabled = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager is null!");
        }
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("SpawnManager is null!");
        }
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

    public void UpdateLives(int currentLives, bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            redLifeImage.sprite = redLifeSprites[currentLives];
            if (currentLives <= 0)
            {
                isPlayerOneDead = true;
            }
        }
        else
        {
            blueLifeImage.sprite = blueLifeSprites[currentLives];
            if (currentLives <= 0)
            {
                isPlayerTwoDead = true;
            }
        }
        if (isPlayerOneDead && isPlayerTwoDead || (!gameManager.GetIsCoOpMode()) && isPlayerOneDead)
        {
            DoGameOver();
        }
    }

    private void DoGameOver()
    {
        restartLevelInstructionsText.enabled = true;
        gameManager.GameOver();
        spawnManager.OnPlayerDeath();
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
