using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text gameOverText;
    [SerializeField]
    private TMP_Text restartLevelInstructionsText;
    [SerializeField]
    private Image lifeImage;
    [SerializeField]
    private Sprite[] lifeSprites;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        gameOverText.text = "";
        restartLevelInstructionsText.enabled = false;
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score;
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
    }

    IEnumerator FlickerGameOver()
    {
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
