using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float stoppingDistance = 1.5f;

    private CharacterController controller;
    private float verticalSpeed;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        Vector3 velocity = Vector3.zero;

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direction);

            if (direction.magnitude > stoppingDistance)
            {
                velocity = direction.normalized * moveSpeed;
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