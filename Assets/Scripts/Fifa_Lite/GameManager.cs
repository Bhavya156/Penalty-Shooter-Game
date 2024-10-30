using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }
    void PauseGame() {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame() {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
