using UnityEngine;

public class EnemyTouch1: MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            HpBahar hp = other.GetComponent<HpBahar>();
            if (hp != null)
            {
                hp.StartCoroutine(hp.takeDamage());
            }
        }
    }
}
