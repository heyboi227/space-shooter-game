using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver = false;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private bool isCoOpMode = false;

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
            if (!isCoOpMode)
            {
                StartNewSinglePlayerGame();
            }
            else
            {
                StartNewCoOpGame();
            }
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

    public void StartNewSinglePlayerGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartNewCoOpGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public bool GetIsCoOpMode()
    {
        return isCoOpMode;
    }
}
