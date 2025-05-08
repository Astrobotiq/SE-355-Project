using UnityEngine;
using UnityEngine.UI;

public class WinZone : MonoBehaviour
{

    public GameObject gameOverText;
    public Canvas canvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 deathPosition = other.transform.position;

            Vector3 screenPoint = Camera.main.WorldToScreenPoint(deathPosition);

            if (gameOverText != null && canvas != null)
            {
                RectTransform canvasRect = canvas.GetComponent<RectTransform>();
                RectTransform textRect = gameOverText.GetComponent<RectTransform>();

                Vector2 canvasPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, canvas.worldCamera, out canvasPos);

                textRect.anchoredPosition = canvasPos + new Vector2(100f, 100f);

                gameOverText.SetActive(true);
            }
        }
    }
}