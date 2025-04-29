using System.Collections.Generic;
using UnityEngine;

public class ArcShooterEnemy : BaseEnemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 8f;
    public float fireCooldown = 2f;
    public Transform[] positions;

    private float lastFireTime;

    protected override void Start()
    {
        base.Start();
        positions = transform.GetComponentsInChildren<Transform>();
    }

    public override void CustomUpdate()
    {
        base.CustomUpdate();
        
        Move(Vector2.zero);

        if (Time.time - lastFireTime >= fireCooldown)
        {
            FireInArc();
            lastFireTime = Time.time;
        }
    }

    void FireInArc()
    {
        foreach (var target in positions)
        {
            Vector2 direction = (Vector2)target.position - (Vector2)firePoint.position;

            // 2. Normalize et (sabit hız için)
            Vector2 normalizedDir = direction.normalized;

            // 3. Mermiyi oluştur
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // 4. Rigidbody2D ile kuvvet ver
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = normalizedDir * fireForce;

            // 5. (Opsiyonel) Mermiyi hedefe doğru döndür
            float angle = Mathf.Atan2(normalizedDir.y, normalizedDir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}