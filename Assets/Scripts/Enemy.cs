using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(TargetFinder))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float stoppingDistance = 1.5f;

    private CharacterController controller;
    private TargetFinder targetFinder;
    private float verticalSpeed;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        targetFinder = GetComponent<TargetFinder>();
    }

    private void Update()
    {
        Health target = targetFinder.CurrentTarget;
        Vector3 velocity = Vector3.zero;

        if (target != null)
        {
            Vector3 direction =
                target.transform.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction);

                if (direction.magnitude > stoppingDistance)
                {
                    velocity = direction.normalized * moveSpeed;
                }
            }
        }

        if (controller.isGrounded && verticalSpeed < 0f)
        {
            verticalSpeed = -2f;
        }

        verticalSpeed += Physics.gravity.y * Time.deltaTime;
        velocity.y = verticalSpeed;

        controller.Move(velocity * Time.deltaTime);
    }
}
