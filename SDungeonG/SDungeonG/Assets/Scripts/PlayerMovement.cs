using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    /*
     * public float speed = 6f;

        private CharacterController controller;

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(x, 0, z);

            controller.Move(move * speed * Time.deltaTime);
        }
    */

    public float speed = 6f;

    private CharacterController controller;
    
    Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        controller.Move(
            moveDirection *
            speed *
            Time.deltaTime
        );
    }

    public void MoveUp()
    {
        moveDirection =
            Vector3.forward;
    }

    public void MoveDown()
    {
        moveDirection =
            Vector3.back;
    }

    public void MoveLeft()
    {
        moveDirection =
            Vector3.left;
    }

    public void MoveRight()
    {
        moveDirection =
            Vector3.right;
    }

    public void StopMove()
    {
        moveDirection =
            Vector3.zero;
    }

}