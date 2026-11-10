using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 25;

    private TeamId sourceTeam;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        float distance = speed * Time.deltaTime;

        RaycastHit[] hits = Physics.RaycastAll(
            transform.position,
            transform.forward,
            distance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore
        );

        System.Array.Sort(
            hits,
            (a, b) => a.distance.CompareTo(b.distance)
        );

        foreach (RaycastHit hit in hits)
        {
            TeamMember hitTeam =
                hit.collider.GetComponentInParent<TeamMember>();

            if (hitTeam != null && hitTeam.Team == sourceTeam)
                continue;

            Health health =
                hit.collider.GetComponentInParent<Health>();

            if (health != null && hitTeam != null)
            {
                health.TakeDamage(damage);
            }

            transform.position = hit.point;
            Destroy(gameObject);
            return;
        }

        transform.position += transform.forward * distance;
    }

    public void Initialize(TeamId team)
    {
        sourceTeam = team;
    }
}