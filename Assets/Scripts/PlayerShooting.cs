using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TeamMember))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 0.25f;

    private float nextShotTime;
    private TeamMember teamMember;

    private void Awake()
    {
        teamMember = GetComponent<TeamMember>();
    }

    private void LateUpdate()
    {
        Mouse mouse = Mouse.current;

        if (mouse == null || bulletPrefab == null || firePoint == null)
            return;

        if (!mouse.leftButton.isPressed || Time.time < nextShotTime)
            return;

        Bullet bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        bullet.Initialize(teamMember.Team);

        nextShotTime = Time.time + fireInterval;
    }
}