using UnityEngine;

public class ShootingEnemy : BaseEnemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 1.5f;
    public float detectionRange = 10f;

    private float lastFireTime;
    private Transform player;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public override void CustomUpdate()
    {
        base.CustomUpdate();

        if (player == null) return;
        
        Move(Vector2.zero); 

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && Time.time - lastFireTime >= fireCooldown)
        {
            ShootAtPlayer();
            lastFireTime = Time.time;
        }
    }

    void ShootAtPlayer()
    {
        if (bulletPrefab == null || firePoint == null) return;

        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f; // Adjust speed as needed
    }
}