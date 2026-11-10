using UnityEngine;

[RequireComponent(typeof(TeamMember))]
public class TargetFinder : MonoBehaviour
{
    [SerializeField] private float searchInterval = 0.25f;
    [SerializeField] private Health currentTarget;

    private TeamMember teamMember;
    private float nextSearchTime;

    public Health CurrentTarget => currentTarget;

    private void Awake()
    {
        teamMember = GetComponent<TeamMember>();
    }

    private void Update()
    {
        if (Time.time < nextSearchTime)
            return;

        nextSearchTime = Time.time + searchInterval;
        FindTarget();
    }

    private void FindTarget()
    {
        currentTarget = null;
        float closestDistance = float.PositiveInfinity;

        Health[] candidates =
            FindObjectsByType<Health>(FindObjectsSortMode.None);

        foreach (Health candidate in candidates)
        {
            if (!candidate.isActiveAndEnabled || !candidate.IsAlive)
                continue;

            TeamMember candidateTeam =
                candidate.GetComponentInParent<TeamMember>();

            if (!teamMember.IsEnemy(candidateTeam))
                continue;

            Vector3 direction =
                candidate.transform.position - transform.position;
            direction.y = 0f;

            float distance = direction.sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentTarget = candidate;
            }
        }
    }
}