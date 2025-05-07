using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject arcProjectilePrefab;
    public GameObject straightProjectilePrefab;

    public float throwForce = 15f;
    public float throwAngle = 30f;

    private bool isStraight = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isStraight = false;
            shoot();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            isStraight = true;
            shoot();
        }
    }
    void shoot()
    {
        if (!isStraight)
        {
            GameObject projectile = Instantiate(arcProjectilePrefab, throwPoint.position, throwPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            float angleInRad = throwAngle * Mathf.Deg2Rad;
            Vector2 fireDirection = new Vector2(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad));

            fireDirection = throwPoint.right * fireDirection.x + throwPoint.up * fireDirection.y;

            rb.linearVelocity = fireDirection.normalized * throwForce;
        }
        else 
        {
            GameObject projectile = Instantiate(straightProjectilePrefab, throwPoint.position, throwPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            rb.linearVelocity = throwPoint.right * throwForce;
        }
    }
}