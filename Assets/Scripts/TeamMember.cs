using UnityEngine;

public enum TeamId
{
    Survivors,
    Infected
}

public class TeamMember : MonoBehaviour
{
    [SerializeField] private TeamId team;

    public TeamId Team => team;

    public bool IsEnemy(TeamMember other)
    {
        return other != null && team != other.team;
    }
}