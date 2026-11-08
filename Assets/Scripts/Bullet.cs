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

        if (Physics.Raycast(
            transform.position,
            transform.forward,
            out RaycastHit hit,
            distance,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore))
        {
            Health health = hit.collider.GetComponentInParent<Health>();
            TeamMember hitTeam = hit.collider.GetComponentInParent<TeamMember>();

            if (health != null &&
                hitTeam != null &&
                hitTeam.Team != sourceTeam)
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