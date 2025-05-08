using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    public GameObject winText;       
    public GameObject winButton;    
    public Canvas canvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 winPosition = other.transform.position;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(winPosition);
           

            if (canvas != null)
            {
                RectTransform canvasRect = canvas.GetComponent<RectTransform>();

                if (winText != null)
                {
                    RectTransform textRect = winText.GetComponent<RectTransform>();
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, canvas.worldCamera, out Vector2 canvasPos);
                    textRect.anchoredPosition = canvasPos + new Vector2(100f, 100f);
                    winText.SetActive(true);
                }

                if (winButton != null)
                {
                    RectTransform buttonRect = winButton.GetComponent<RectTransform>();
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, canvas.worldCamera, out Vector2 canvasPos);
                    buttonRect.anchoredPosition = canvasPos + new Vector2(100f, 40f); 
                    winButton.SetActive(true);
                }

            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
