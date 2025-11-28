using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    [Header("Batas Map (Camera Limits)")]
    public Vector2 minPosition; 
    public Vector2 maxPosition; 
    public bool limitCamera = true; 

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (limitCamera)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);
        }

        transform.position = smoothedPosition;
    }
}