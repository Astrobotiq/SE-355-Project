using UnityEngine;

public class ChasingEnemy : BaseEnemy
{
    public float chaseRange = 5f;

    public override void CustomUpdate()
    {
        base.CustomUpdate(); // gravity, ground check burada

        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < chaseRange)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            Move(new Vector2(dir.x, 0)); // sadece yatay takip
            FlipTowards(player.transform.position);
        }
        else
        {
            Move(Vector2.zero); // düşerken sadece düş
        }
    }
}