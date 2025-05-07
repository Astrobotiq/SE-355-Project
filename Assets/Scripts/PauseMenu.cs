using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject HUDPanelUI;
    public GameObject GameOverUI;
    private bool isPaused = false;
    private bool isMuted = false;
    public GameObject muteEffectUI;
    void Start()
    {
        pauseMenuUI.SetActive(false);        
    }
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        HUDPanelUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        HUDPanelUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenOptionsMenu()
    {
        Debug.Log("Options Menu Opened");
    }

    public void SwitchMute()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            muteEffectUI.SetActive(true);
            AudioListener.volume = 0f;
        }
        else
        {
            muteEffectUI.SetActive(false);
            AudioListener.volume = 1f;
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        isPaused = false;
        GameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverMenu()
    {
        pauseMenuUI.SetActive(false);
        HUDPanelUI.SetActive(false);
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
