using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver = false;
    [SerializeField]
    private GameObject panel;

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            StartNewGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P) && !isGameOver)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
