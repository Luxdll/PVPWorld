using UnityEngine;

[RequireComponent(typeof(TeamMember), typeof(TargetFinder))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float attackInterval = 1f;

    private TeamMember teamMember;
    private TargetFinder targetFinder;
    private float nextAttackTime;

    private void Awake()
    {
        teamMember = GetComponent<TeamMember>();
        targetFinder = GetComponent<TargetFinder>();
    }

    private void Update()
    {
        Health target = targetFinder.CurrentTarget;

        if (target == null || Time.time < nextAttackTime)
            return;

        Vector3 direction =
            target.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > attackRange * attackRange)
            return;

        TeamMember targetTeam =
            target.GetComponentInParent<TeamMember>();

        if (!teamMember.IsEnemy(targetTeam))
            return;

        target.TakeDamage(damage);
        nextAttackTime = Time.time + attackInterval;
    }
}
