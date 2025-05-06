using UnityEngine;

public class SimpleMovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 0.5f;

    private Vector3 target;
    private float waitTimer;
    private Vector3 lastPosition;

    void Start()
    {
        target = pointB.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                target = (target == pointA.position) ? pointB.position : pointA.position;
                waitTimer = 0f;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = transform.position - lastPosition;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().bounds.size, 0f);
        foreach (var hit in hits)
        {
            if (hit.attachedRigidbody != null && hit.transform != transform)
            {
                // Sadece yukarıdan temas edenleri taşı
                if (hit.bounds.min.y > GetComponent<Collider2D>().bounds.max.y - 0.05f)
                {
                    hit.transform.position += deltaMovement;
                }
            }
        }

        lastPosition = transform.position;
    }
}
