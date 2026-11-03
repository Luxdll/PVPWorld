using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    private CharacterController controller;
    private float verticalSpeed;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 direction = Vector3.zero;
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            direction.x =
                (keyboard.dKey.isPressed ? 1f : 0f) -
                (keyboard.aKey.isPressed ? 1f : 0f);

            direction.z =
                (keyboard.wKey.isPressed ? 1f : 0f) -
                (keyboard.sKey.isPressed ? 1f : 0f);
        }

        direction = Vector3.ClampMagnitude(direction, 1f);

        if (controller.isGrounded && verticalSpeed < 0f)
        {
            verticalSpeed = -2f;
        }

        verticalSpeed += Physics.gravity.y * Time.deltaTime;

        Vector3 velocity = direction * moveSpeed;
        velocity.y = verticalSpeed;

        controller.Move(velocity * Time.deltaTime);
    }
}