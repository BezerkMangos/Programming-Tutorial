using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; // Using the package we installed in Lab 1
using UnityEngine;
using UnityEngine.InputSystem.HID;


public class PlayerController : MonoBehaviour
{
    PlayerMovement inputAction;
    Vector2 move;
    Vector2 rotation;
    Rigidbody rb;

    private float distanceToGround;
    bool onGround;
    public float jump = 5f;
    public float walk = 5f;
    public Camera playerCam;
    Vector3 cameraRotation;

    private Animator playerAnimation;
    private bool isWalking = false;

    public GameObject projectile;
    public Transform porjectilePosition;

    //Health Testing
    CharacterStats cs;
    


    private void Awake() // Unity's First Methond on Runtime
    {
        inputAction = new PlayerMovement();
        
        //Moving action
        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        //Jumping Action
        inputAction.Player.Jump.performed += cntxt => Jump();

        //Looking Actions
        inputAction.Player.Look.performed += cntxt => rotation = cntxt.ReadValue<Vector2>();
        inputAction.Player.Look.canceled += cntxt => rotation = Vector2.zero;

        //Shooting Action
        inputAction.Player.Shoot.performed += cntxt => Shoot();

        //Health test
        cs = GetComponent<CharacterStats>();
        inputAction.Player.TakeDamage.performed += cntxt => cs.TakeDamage(2);
        //Getting rigidbody
        rb = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<Animator>();

        


        cameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        Cursor.lockState = CursorLockMode.Locked;

        distanceToGround = GetComponent<Collider>().bounds.extents.y;

    }

    private void OnEnable() // Unity's Second Methond on Runtime
    {
        inputAction.Player.Enable();
    }

    private void Update() // Unity's updated methonds, updates every frame
    {
        //Movement
        transform.Translate(Vector3.forward * move.y * Time.deltaTime * walk, Space.Self);
        transform.Translate(Vector3.right * move.x * Time.deltaTime * walk, Space.Self);

        //Camera Rotation
        cameraRotation = new Vector3(cameraRotation.x + rotation.y, cameraRotation.y + rotation.x, cameraRotation.z);
        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y,transform.rotation.z);

        //JumpCheck
        onGround = Physics.Raycast(transform.position,-Vector3.up,distanceToGround);

    }
    private void LateUpdate()
    {
        //playerCam.transform.eulerAngles = new Vector3(cameraRotation.x, cameraRotation.y, cameraRotation.z);
        playerCam.transform.rotation = Quaternion.Euler(cameraRotation);
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
    }

    private void Jump()
    {
        
        if (onGround == true)
        {
            rb.velocity = new Vector3(rb.velocity.x,jump, rb.velocity.z); //rb.velocity.z Gets the current velocity of this rigidbody z axis
        }
        
    }
    private void Shoot()
    {
        Rigidbody rbBullet = Instantiate(projectile, porjectilePosition.position, Quaternion.identity).GetComponent<Rigidbody>();
        rbBullet.AddForce(Vector3.forward * 32f, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position,-Vector3.up * distanceToGround);
    }










}
