using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform visual;

    private Camera viewCamera;
    private Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    private void Awake()
    {
        viewCamera = Camera.main;
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;

        if (mouse == null || viewCamera == null || visual == null)
            return;

        Ray ray = viewCamera.ScreenPointToRay(
            mouse.position.ReadValue()
        );

        if (!groundPlane.Raycast(ray, out float distance))
            return;

        Vector3 aimPoint = ray.GetPoint(distance);
        Vector3 direction = aimPoint - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            visual.rotation = Quaternion.LookRotation(direction);
        }
    }
}