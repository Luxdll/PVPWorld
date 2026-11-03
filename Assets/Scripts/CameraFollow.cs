using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(0f, 1f)] private float followAmount = 0.5f;
    [SerializeField] private float arenaHalfLength = 30f;
    [SerializeField, Min(0f)] private float edgePadding = 2f;

    private Camera viewCamera;

    private void Awake()
    {
        viewCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        float halfViewWidth =
            viewCamera.orthographicSize * viewCamera.aspect;

        float cameraLimit =
            Mathf.Max(0f, arenaHalfLength - halfViewWidth);

        Vector3 desiredPosition = transform.position;
        float safeHalfWidth = Mathf.Max(0f, halfViewWidth - edgePadding);
        float trackingX = Mathf.Clamp(
            target.position.x * followAmount,
            target.position.x - safeHalfWidth,
            target.position.x + safeHalfWidth
        );
        desiredPosition.x = Mathf.Clamp(
            trackingX,
            -cameraLimit,
            cameraLimit
        );

        transform.position = desiredPosition;
    }
}
