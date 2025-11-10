using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 public static GameManager Instance;
    public float worldSpeed;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

    }
    public void PauseGame()
    {
       if (UIController.Instance.pausePanel.activeSelf==false)
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
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
