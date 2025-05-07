using UnityEngine;

public class MeteParallaxEffect : MonoBehaviour
{
    private Vector3 lastCameraPosition;
    public Transform cameraTransform;
    public Vector2 parallaxMultiplier = new Vector2(0.5f, 0.5f); // X and Y speed relative to camera

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier.x, deltaMovement.y * parallaxMultiplier.y, 0f);
        lastCameraPosition = cameraTransform.position;
    }
}
