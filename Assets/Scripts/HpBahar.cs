using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;

public class HpBahar : MonoBehaviour
{
    public int maxHP = 3;
    public int currHP;
    public bool invulnerable = false;
    SpriteRenderer spRend;
    public Image healthBarImage;

    public GameObject deathEffectPrefab;
    public GameObject gameOverText;
    public Canvas canvas;


    void Start()
    {
        currHP = maxHP;
        spRend = GetComponent<SpriteRenderer>();
        UpdateHealthBar();
    }

    void Update()
    {
        if (invulnerable)
        {
            Debug.Log("Player is unvulnerable");
        }
    }

    /* private void OnTriggerEnter2D(Collider2D other)
     {
         if (invulnerable) return;

         if (other.CompareTag("Enemy"))
         {
             takeDamage();            
         }
     }*/
    public IEnumerator takeDamage()
    {
        if (currHP > 1 && !invulnerable)
        {
            
            if (spRend != null)
            {
                invulnerable = true;
                currHP -= 1;
                Debug.Log("Current hp: " + currHP);
                Color originalColor = spRend.color;
                
                spRend.color = new Color(1f, 0.2f, 0.2f);
                yield return new WaitForSeconds(0.5f);
                spRend.color = originalColor;
                invulnerable = false;
            }
            
            
        }
        else
        {
            Vector3 deathPosition = transform.position;

            Vector3 screenPoint = Camera.main.WorldToScreenPoint(deathPosition);

            if (gameOverText != null && canvas != null)
            {
                RectTransform canvasRect = canvas.GetComponent<RectTransform>();
                RectTransform textRect = gameOverText.GetComponent<RectTransform>();

                Vector2 canvasPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, canvas.worldCamera, out canvasPos);

                textRect.anchoredPosition = canvasPos + new Vector2(70f, 100f);

                gameOverText.SetActive(true);
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


            Destroy(transform.gameObject);
        }
        UpdateHealthBar();

    }
    void UpdateHealthBar()
    {
        if (healthBarImage != null)
            healthBarImage.fillAmount = (float)currHP / maxHP;
    }
}
