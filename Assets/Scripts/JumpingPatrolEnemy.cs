using UnityEngine;

public class JumpingPatrolEnemy : BaseEnemy
{
    [Header("Patrol Settings")]
    public Transform pointA;
    public Transform pointB;
    public float arrivalThreshold = 0.1f;

    [Header("Jump Settings")]
    public float minJumpForce = 8f;
    public float maxJumpForce = 12f;
    public float jumpCooldown = 2f;

    private Transform currentTarget;
    private float lastJumpTime;

    protected override void Start()
    {
        base.Start();
        currentTarget = pointB;
        lastJumpTime = Time.time;
    }

    public override void CustomUpdate()
    {
        base.CustomUpdate(); // gravity, ground check

        if (currentTarget == null || pointA == null || pointB == null) return;

        // Devriye yönü
        Vector2 dir = (currentTarget.position - transform.position).normalized;
        Move(new Vector2(dir.x, 0));

        // Zıplama işlemi
        if (Time.time >= lastJumpTime + jumpCooldown)
        {
            float jumpForce = Random.Range(minJumpForce, maxJumpForce);
            verticalVelocity = jumpForce; // düzeltildi
            lastJumpTime = Time.time;
        }


        // Hedefe ulaşınca yön değiştir
        if (Vector2.Distance(transform.position, currentTarget.position) < arrivalThreshold)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }

        FlipTowards(currentTarget.position);
    }
}