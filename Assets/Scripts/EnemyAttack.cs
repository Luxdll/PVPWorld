using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Health target;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float attackInterval = 1f;

    private float nextAttackTime;

    private void Update()
    {
        if (target == null)
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