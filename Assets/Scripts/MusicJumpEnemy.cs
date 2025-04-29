using UnityEngine;

public class MusicJumpEnemy : BaseEnemy
{
    [Header("Music Jump Settings")]
    public float jumpForce = 12f;
    public float eventCooldown = 0.2f;

    private float lastJumpTime;

    protected void OnEnable()
    {
        MusicBeatManager.OnBeatDetected += HandleBeat;
    }

    protected void OnDisable()
    {
        MusicBeatManager.OnBeatDetected -= HandleBeat;
    }

    private void HandleBeat()
    {
        if (!isGrounded || Time.time - lastJumpTime < eventCooldown) return;

        verticalVelocity = jumpForce;
        lastJumpTime = Time.time;
    }
    
    public override void CustomUpdate()
    {
        base.CustomUpdate(); // gravity, bounds, ground check burada zaten çağrılıyor

        // Beat yoksa zıplama yapılmıyor, sadece düşey hareket
        Move(Vector2.zero); // X yönü yok
    }
}