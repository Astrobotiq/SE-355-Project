using UnityEngine;

public class PatrolEnemy : BaseEnemy
{
    [Header("Patrol Settings")]
    public Transform pointA;
    public Transform pointB;
    public float arrivalThreshold = 0.1f;

    private Transform currentTarget;

    protected override void Start()
    {
        base.Start();
        currentTarget = pointB;
    }

    public override void CustomUpdate()
    {
        base.CustomUpdate(); // gravity & ground check

        if (currentTarget == null || pointA == null || pointB == null) return;

        // Yön belirle
        Vector2 dir = (currentTarget.position - transform.position).normalized;

        // Yalnızca yatay eksende ilerle
        Move(new Vector2(dir.x, 0));

        // Hedefe ulaştıysa yön değiştir
        if (Vector2.Distance(transform.position, currentTarget.position) < arrivalThreshold)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }

        // Sprite yönü değiştir
        FlipTowards(currentTarget.position);
    }
}