using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //=======================
    //Variable Initialisation
    //=======================

    //Movement

    public float MoveSpeed = 10.0f;
    private Vector2 movementInput;
    
    //Camera Movement
    
    public Camera camera;

    public Transform head;
    public float Senstivity = 10.0f;
    
    private Vector2 lookInput;
    
    private float pitch = 0.0f;
    private float yaw = 0.0f;

    //Raycasting

    public float m_RayDistance = 10.0f;

    private bool m_RayHit = false;
    private Vector3 m_HitPoint = Vector3.zero;
    private Vector3 m_HitNormal = Vector3.zero;
    private bool m_Grounded = false;

    private CharacterController characterController;

    //============
    //Main Program
    //============

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Look
        pitch -= lookInput.y * Time.deltaTime * Senstivity;
        yaw += lookInput.x * Time.deltaTime * Senstivity;

        pitch = Mathf.Clamp(pitch, -90f, 90f); //limits looking up and down to less than 180d

        head.localRotation = Quaternion.identity; //Reset rotation
        head.Rotate(pitch, yaw, 0);

        //Move
        Vector3 movement = head.forward * movementInput.y;
        movement += head.right * movementInput.x;
        movement.y = 0.0f; //Stops us from flying
        movement.Normalize(); //Turns this into a Unit Vector and stops us from moving faster diagonally

        characterController.Move(movement * Time.deltaTime * MoveSpeed);       
    }

    void DoRaycast()
    {
        RaycastHit hitInfo; //gives us information about what we hit (if anything)
        Ray ray = new Ray(transform.position, head.forward);

        //Do the raycast. Store the information in hitInfo
        m_RayHit = Physics.Raycast(ray, out hitInfo, m_RayDistance);

        if (m_RayHit)
        {
            //m_HitPoint = hitInfo.point;     //Store the position that our ray collided with the object
            //m_HitNormal = hitInfo.normal;   //Store the surface normal of the object
            //m_Grounded = Vector3.Dot(Vector3.up, hitInfo.normal) > 0.5f; //Bit of a magic number here. Just trust me on this one.

            Debug.Log("The ray hit" + hitInfo.collider.name);
        }
        else
        {
            Debug.Log("The ray hit nothing!");
        }
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
        Debug.Log("Fire");

        DoRaycast();

    }

    public void OnScope(InputAction.CallbackContext context)
    {
        //Need to change this for a better system, can get stuck zoomed in

        Debug.Log("Scope");
        if (camera.fieldOfView != 20)
        {
            camera.fieldOfView = 20;
        }
        else
        {
            camera.fieldOfView = 60;
        }       
    }
}
