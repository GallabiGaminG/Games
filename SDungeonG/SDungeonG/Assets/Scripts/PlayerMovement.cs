using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Settings")]
    public float speed = 6f;

    private CharacterController controller;

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
    Vector3 keyboardMove = touchMoveDirection;

    // Keyboard support

    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    {
            keyboardMove = Vector3.forward;
    }

    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
    {
            keyboardMove = Vector3.back;
    }

    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    {
            keyboardMove = Vector3.left;
    }

    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    {
            keyboardMove = Vector3.right;
    }

    if (keyboardMove.sqrMagnitude > 1f)
    {
            keyboardMove.Normalize();
    }

    controller.Move(keyboardMove * speed * Time.deltaTime);

        // 2) Mobile UI button movement.
        Vector3 finalMove = keyboardMove;

        // If a mobile direction button is pressed, use it.
        if (touchMoveDirection.sqrMagnitude > 0.01f)
        {
            finalMove = touchMoveDirection;
        }

        // Prevent faster diagonal movement.
        if (finalMove.sqrMagnitude > 1f)
        {
            finalMove.Normalize();
        }

        controller.Move(finalMove * speed * Time.deltaTime);
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