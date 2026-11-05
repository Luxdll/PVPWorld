using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 0.25f;

    private float nextShotTime;

    private void LateUpdate()
    {
        Mouse mouse = Mouse.current;

        if (mouse == null || bulletPrefab == null || firePoint == null)
            return;

        if (!mouse.leftButton.isPressed || Time.time < nextShotTime)
            return;

        Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        nextShotTime = Time.time + fireInterval;
    }
}