using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Hp : MonoBehaviour
{
    public int maxHP = 3;
    public int currHP;
    public bool invulnerable = false;
    SpriteRenderer spRend;

    void Start()
    {
        currHP = maxHP;
        spRend = GetComponent<SpriteRenderer>();
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
        if (currHP > 0)
        {
            invulnerable = true;
            currHP -= 1;
            Debug.Log("Current hp: " + currHP);
            if (spRend != null)
            {
                Color originalColor = spRend.color;
                spRend.color = Color.red;
                yield return new WaitForSeconds(1f);
                spRend.color = originalColor;
            }

            invulnerable = false;
        }
        else 
        {
            /*GameObject managerObj = GameObject.Find("GameManager");
            if (managerSc != null) 
            {
                managerSc.GameOverMenu();
            }
            Debug.Log("GameOver");*/
        }
       
    }
}
