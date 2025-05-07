using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private bool isMuted = false;
    public GameObject muteEffectUI;
    public void StartGame()
    {
        SceneManager.LoadScene("PlayerControllerWithRaycast");
    }

    public void OpenOptions()
    {
        Debug.Log("Options menu opened.");
    }

    public void ExitGame()
    {
        Application.Quit();
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
}