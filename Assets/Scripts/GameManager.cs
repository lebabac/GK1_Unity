using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro; // cần cho TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Speed")]
    public float worldSpeed;

    [Header("Score System")]
    public int score = 0;
    public int scoreToWin = 30;
    [SerializeField] private TMP_Text scoreText; // kéo Text (TMP) từ UI vào đây trong Inspector

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    // === PAUSE GAME ===
    public void PauseGame()
    {
        if (UIController.Instance.pausePanel.activeSelf == false)
        {
            Time.timeScale = 0f;
            UIController.Instance.pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            UIController.Instance.pausePanel.SetActive(false);
        }
    }

    // === SCORE MANAGEMENT ===
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score >= scoreToWin)
        {
            StartCoroutine(WinGame());
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // === SCENE CONTROL ===
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameWin");
    }
}
