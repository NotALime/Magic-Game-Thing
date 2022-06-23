using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    public float walkSpeed;

    public float jumpForce;
    public Transform groundCheck;
    public LayerMask ground;
    bool isGrounded;

    public float sensitivity;

    public Transform camAnchor;
    public Camera cam;

    public SpriteRenderer playerSR;

    Vector3 moveInput;

    Vector3 movement;
    Vector3 jumpingInput;

    public Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.01f, ground);
        Jump();
        HandleAnimations();
    }

    void HandleAnimations()
    {
        if (moveInput.z != 0)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
        if (moveInput.x != 0)
        {
            anim.SetBool("MovingSideways", true);
        }
        else
        {
            anim.SetBool("MovingSideways", false);
        }

        if (moveInput.x < -0.01f)
        {
            playerSR.flipX = true;
        }
        else if (moveInput.x > 0.01f)
        {
            playerSR.flipX = false;
        }

        if (rb.velocity.y > 0.1f || rb.velocity.y < -0.1f)
        {
            anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Falling", false);
        }
    }

    private void FixedUpdate()
    {
        BasicMovement();
    }

    void BasicMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.z = Input.GetAxisRaw("Vertical");
        movement = transform.right * moveInput.x + transform.forward * moveInput.z * walkSpeed;

        Debug.Log(moveInput.ToString());
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        CameraMovement();
    }
    void Jump()
    {
        if (isGrounded == true)
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.5f, rb.velocity.y, rb.velocity.z);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Debug.Log("Jumped");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z);
        }
    }

    float xRotation = 0f;

    void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
       
       // xRotation -= mouseY;
       // xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camAnchor.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
