using UnityEngine;

using UnityEngine.InputSystem;



public class FirstPersonMovement : MonoBehaviour

{

    public float moveSpeed = 5f;

    public float mouseSensitivity = 0.1f;

    public float jumpForce = 5f;

    public float groundCheckDistance = 1.1f;

    public LayerMask groundLayer;



    float xRotation = 0f;

    Camera playerCamera;

    Rigidbody rb;



    Vector2 moveInput;

    Vector2 lookInput;

    bool jumpPressed;



    void Start()

    {

        playerCamera = GetComponentInChildren<Camera>();

        rb = GetComponent<Rigidbody>();



        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

    }



    void Update()

    {

        Look();

    }



    void FixedUpdate()

    {

        Move();

        Jump();

    }



    public void OnMove(InputValue value)

    {

        moveInput = value.Get<Vector2>();

    }



    public void OnLook(InputValue value)

    {

        lookInput = value.Get<Vector2>();

    }



    public void OnJump(InputValue value)

    {

        if (value.isPressed)

            jumpPressed = true;

    }



    void Move()

    {

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        Vector3 velocity = move * moveSpeed;



        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

    }



    void Jump()

    {

        if (jumpPressed && IsGrounded())

        {

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);

            jumpPressed = false;

        }

    }



    bool IsGrounded()

    {

        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

    }



    void Look()

    {

        float mouseX = lookInput.x * mouseSensitivity * 100f * Time.deltaTime;

        float mouseY = lookInput.y * mouseSensitivity * 100f * Time.deltaTime;



        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);



        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

    }

}