using System.Collections;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    public void takeDamage()
    {
        if (spriteRenderer != null)
        {
            StartCoroutine(ChangeEnemyColorTemporarily(spriteRenderer));
        }
    }

    IEnumerator ChangeEnemyColorTemporarily(SpriteRenderer renderer)
    {
        Color originalColor = renderer.color;
        renderer.color = Color.black;

        yield return new WaitForSeconds(0.1f);

        renderer.color = originalColor;
    }
}
