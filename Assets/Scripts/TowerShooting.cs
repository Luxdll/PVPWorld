using UnityEngine;

[RequireComponent(typeof(TargetFinder), typeof(TeamMember))]
public class TowerShooting : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackRange = 8f;
    [SerializeField] private float fireInterval = 1f;

    private TargetFinder targetFinder;
    private TeamMember teamMember;
    private float nextShotTime;

    private void Awake()
    {
        targetFinder = GetComponent<TargetFinder>();
        teamMember = GetComponent<TeamMember>();
    }

    private void Start()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError(
                "TowerShooting: назначь пулю и точку выстрела.",
                this
            );

            enabled = false;
        }
    }

    private void Update()
    {
        Health target = targetFinder.CurrentTarget;

        if (target == null || Time.time < nextShotTime)
            return;

        Vector3 distanceToTarget =
            target.transform.position - transform.position;
        distanceToTarget.y = 0f;

        if (distanceToTarget.sqrMagnitude > attackRange * attackRange)
            return;

        TeamMember targetTeam =
            target.GetComponentInParent<TeamMember>();

        if (!teamMember.IsEnemy(targetTeam))
            return;

        Collider targetCollider = target.GetComponent<Collider>();

        Vector3 aimPoint = targetCollider != null
            ? targetCollider.bounds.center
            : target.transform.position;

        Vector3 direction = aimPoint - firePoint.position;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Bullet bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

        bullet.Initialize(teamMember.Team);
        nextShotTime = Time.time + fireInterval;
    }
}