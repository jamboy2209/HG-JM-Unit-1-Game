using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform head;
    public float Senstivity = 10.0f;
    public float MoveSpeed = 10.0f;
    private Vector2 movementInput;
    private Vector2 lookInput;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    private CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Look
        pitch -= lookInput.y * Time.deltaTime * Senstivity;
        yaw += lookInput.x * Time.deltaTime * Senstivity;

        pitch = Mathf.Clamp(pitch, -90f, 90f);
        //yaw = Mathf.Clamp(yaw, -90f, 90f);

        head.localRotation = Quaternion.identity; //Reset rotation
        head.Rotate(pitch, yaw, 0);

        //Move
        Vector3 movement = head.forward * movementInput.y;
        movement += head.right * movementInput.x;
        movement.y = 0.0f; //Stops us from flying
        movement.Normalize(); //Turns this into a Unit Vector and stops us from moving faster diagonally

        characterController.Move(movement * Time.deltaTime * MoveSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {

    }
    public void OnAttack(InputAction.CallbackContext context)
    {

    }
}
