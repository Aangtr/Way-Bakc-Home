using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target (Player)")]
    public Transform target; // Player

    [Header("Follow Settings")]
    public float smoothSpeed = 0.125f;   // Độ mượt
    public Vector3 offset;               // Khoảng cách từ camera đến player

    [Header("Map Clamp")]
    public bool useBounds = false;       // Bật/Tắt giới hạn map
    public float minX, maxX;
    public float minY, maxY;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        if (useBounds)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);
        }

        transform.position = new Vector3(
            smoothedPosition.x,
            smoothedPosition.y,
            transform.position.z
        );
    }
}
