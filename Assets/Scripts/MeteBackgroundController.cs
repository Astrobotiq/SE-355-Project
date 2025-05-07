using UnityEngine;

public class MeteBackgroundController : MonoBehaviour
{
    private float startPos;
    private float length;

    public GameObject cam;
    public float parallaxEffect = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float cameraX = cam.transform.position.x;

        // Move background based on parallax effect
        float distance = cameraX * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // Calculate the "movement" relative to background scroll
        float movement = cameraX * (1 - parallaxEffect);

        // Looping logic: reposition background when threshold is reached
        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
