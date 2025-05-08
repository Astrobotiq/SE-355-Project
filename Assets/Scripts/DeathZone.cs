using UnityEngine;
using UnityEngine.UI;

public class DeathZone : MonoBehaviour
{
    public GameObject deathEffectPrefab;
    public GameObject gameOverText;
    public GameObject retryButton;
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

                textRect.anchoredPosition = canvasPos + new Vector2(70f, 100f); 

                gameOverText.SetActive(true);
                retryButton.SetActive(true);
            }

            if (deathEffectPrefab != null)
            {
                Vector3 spawnPos = new Vector3(deathPosition.x, deathPosition.y, 0); 
                GameObject effect = Instantiate(deathEffectPrefab, spawnPos, Quaternion.identity);
                

                ParticleSystem ps = effect.GetComponent<ParticleSystem>();
                if (ps == null)
                    ps = effect.GetComponentInChildren<ParticleSystem>();

                if (ps != null)
                    ps.Play();
            }


            Destroy(other.gameObject);
        }
    }
}