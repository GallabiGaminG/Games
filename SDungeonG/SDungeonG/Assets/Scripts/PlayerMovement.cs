using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Settings")]
    public float speed = 6f;

    private CharacterController controller;
    public VirtualJoystick joystick;

    // Move direction coming from mobile UI buttons.
    private Vector3 touchMoveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Safety reset when scene starts.
        touchMoveDirection = Vector3.zero;
    }

    void Update()
    {
        Vector3 finalMove = Vector3.zero;

        // 1) Joystick
        if (joystick != null &&
            joystick.Direction.sqrMagnitude > 0.01f)
        {
            finalMove =
                new Vector3(
                    joystick.Direction.x,
                    0f,
                    joystick.Direction.y
                );
        }

        // 2) Mobile buttons
        else if (touchMoveDirection.sqrMagnitude > 0.01f)
        {
            finalMove = touchMoveDirection;
        }

        // 3) Keyboard
        else
        {
            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.UpArrow))
            {
                finalMove = Vector3.forward;
            }

            if (Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.DownArrow))
            {
                finalMove = Vector3.back;
            }

            if (Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.LeftArrow))
            {
                finalMove = Vector3.left;
            }

            if (Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.RightArrow))
            {
                finalMove = Vector3.right;
            }
        }

        if (finalMove.sqrMagnitude > 1f)
        {
            finalMove.Normalize();
        }

        controller.Move(
            finalMove *
            speed *
            Time.deltaTime
        );
    }

    // These public methods are called by UI Button / EventTrigger.

    public void MoveUp()
    {
        touchMoveDirection = Vector3.forward;
    }

    public void MoveDown()
    {
        touchMoveDirection = Vector3.back;
    }

    public void MoveLeft()
    {
        touchMoveDirection = Vector3.left;
    }

    public void MoveRight()
    {
        touchMoveDirection = Vector3.right;
    }

    public void StopMove()
    {
        touchMoveDirection = Vector3.zero;
    }
}