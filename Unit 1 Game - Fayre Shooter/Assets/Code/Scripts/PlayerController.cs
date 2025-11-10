//using Unity.Collections;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    //=======================
    //Variable Initialisation
    //=======================

    //Level loading

    //public GameObject[] duckPlatforms;

    public int levelNo = 2;
    
    //Player Movement

    public float MoveSpeed = 10.0f;
    private Vector2 movementInput;
    
    //Camera Movement
    
    public new Camera camera;

    public Transform head;
    public float Senstivity = 10.0f;
    
    private Vector2 lookInput;
    
    private float pitch = 0.0f;
    private float yaw = 0.0f;

    //Shooting

    private int cooldown;
    public int cooldownTime = 30;

    public float gunRange = 10.0f;

    private bool gunHit = false;

    public float bulletForce = 10.0f;

    //Scoping

    public GameObject gun;

    private CharacterController characterController;

    //gunshot sound

    public AudioSource source;
    public AudioClip gunshot;

    //pitch rand range

    public float lowPitchRange = 1.0f;
    public float highPitchRange = 3.0f;

    //============
    //Main Program
    //============

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //intitialises character controller component
        characterController = GetComponent<CharacterController>();

        //locks cursor to screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //=========
        // Looking
        //=========

        //modifies the mouse looking input from input system by normalising with time.deltaTime and the sensitivity variable
        pitch -= lookInput.y * Time.deltaTime * Senstivity;
        yaw += lookInput.x * Time.deltaTime * Senstivity;
        
        //limits looking up and down to less than 180d
        pitch = Mathf.Clamp(pitch, -90f, 90f); 
        
        //Reset rotation
        head.localRotation = Quaternion.identity; 
        head.Rotate(pitch, yaw, 0);


        //==========
        // Movement
        //==========

        //forward movement = where you are looking multiplied by the forward input value
        Vector3 movement = head.forward * movementInput.y;

        //lateral movement = perpendicular to where you are looking multiplied by sideways input value
        movement += head.right * movementInput.x;

        //Stops us from flying
        movement.y = 0.0f; 

        //Turns this into a Unit Vector and stops us from moving faster diagonally
        movement.Normalize(); 


        //exectues the movement
        characterController.Move(movement * Time.deltaTime * MoveSpeed);


        //=============================
        // limit rate of fire cooldown
        //=============================

        if (cooldown > 0)
        {
            cooldown--;
        }
    }

    //casts  a ray which is used for detecting and acting on the ducks/buttons
    void DoRaycast()
    {
        //gives us information about what we hit (if anything)
        RaycastHit hitInfo; 

        //creates a new ray where we are looking, from the center of our head (which is the camera)
        Ray ray = new Ray(head.position, head.forward);

        //Plays a gunshot sound
        source.PlayOneShot(gunshot, 0.5f);

        //randomises the pitch of the next gunshot
        source.pitch = Random.Range(lowPitchRange, highPitchRange);

        //Do the raycast. Store the information in hitInfo
        gunHit = Physics.Raycast(ray, out hitInfo, gunRange);

        if (gunHit)
        {
            //Debug.Log("The ray hit " + hitInfo.collider.name);

            //stores the hit gameObject's data
            GameObject target = hitInfo.collider.gameObject;

            //checks the Tag of the target, and if it is a physics object (Duck)...
            if (target.CompareTag("Physics Object"))
            {
                //... Add a force in the direction we are looking
                target.GetComponent<Rigidbody>().AddForce(head.forward * bulletForce);

            }
            //if not a physics object, but is a Reset Button
            else if (target.CompareTag("Reset Button"))
            {   
                //checks for each level of button and loads the appropriate scene
                if (target.name == "Lvl 1 Button")
                {
                    SceneManager.LoadScene("Fairground Level 1");
                } 
                else if (target.name == "Lvl 2 Button")
                {
                    SceneManager.LoadScene("Fairground Level 2");
                }
                else
                {
                    SceneManager.LoadScene("Fairground Level 3");
                }
            }
            else
            {
                //Debug.Log(target.name);
            }
        }
            
        else
        {
            //Debug.Log("The ray hit nothing!");
        }
    }

    //When input system detects a movement input, runs this function
    public void OnMove(InputAction.CallbackContext context)
    {
        //context contains the data of the movement input, in this case a 2D vector of forward and lateral movement
        movementInput = context.ReadValue<Vector2>();
    }

    //When input system detects a looking around input, runs this function
    public void OnLook(InputAction.CallbackContext context)
    {
        //context contains the data of the looking around input, in this case a 2D vector of horizontal and vertical mouse movement
        lookInput = context.ReadValue<Vector2>();
    }

    //When input system detects an attack input, runs this function
    public void OnAttack(InputAction.CallbackContext context)
    {
        //checks if the cooldown has expired and for the value from the input system
        if (cooldown <= 0 && context.ReadValue<float>() > 0) {
            
            //Debug.Log("Fire");

            //runs a raycast function.
            DoRaycast();

            //resets the ccooldown
            cooldown = cooldownTime;            
        }
    }
    
    //Scopes in the gun
    public void OnScope(InputAction.CallbackContext context)
    {        
        //Debug.Log("Scope");
        //Debug.Log(cooldown);

        //if right mouse button held down
        if (context.ReadValue<float>() > 0)
        {
            //reduces FOV and sensitivity for zoomed in feeling
            camera.fieldOfView = 20;
            Senstivity = 30;

            //moves the gun and rotates it to a scoped in fashion
            gun.transform.localPosition = new Vector3(0.0f, -0.15f, 1.35f);
            gun.transform.localEulerAngles = new Vector3(0f, 0f ,0f);
        }
        else
        {
            //resets FOV and sensitivity
            camera.fieldOfView = 60;
            Senstivity = 60;

            //resets gun position and rotation
            gun.transform.localPosition = new Vector3(0.65f, -0.3f, 0.85f);
            gun.transform.localEulerAngles = new Vector3(-5f, -5f, 0f);
        }       
    }
}
