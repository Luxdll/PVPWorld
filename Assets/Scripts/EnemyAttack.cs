using UnityEngine;

[RequireComponent(typeof(TeamMember))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Health target;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float attackInterval = 1f;

    private TeamMember teamMember;
    private TeamMember targetTeam;
    private float nextAttackTime;

    private void Awake()
    {
        teamMember = GetComponent<TeamMember>();

        if (target != null)
        {
            targetTeam = target.GetComponentInParent<TeamMember>();
        }
    }

    private void Update()
    {
        if (target == null || !teamMember.IsEnemy(targetTeam))
            return;

        Vector3 direction =
            target.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > attackRange * attackRange)
            return;

        if (Time.time < nextAttackTime)
            return;

        target.TakeDamage(damage);
        nextAttackTime = Time.time + attackInterval;
    }
}