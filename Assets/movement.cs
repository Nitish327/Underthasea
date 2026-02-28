using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    float xRotation = 0f;
    Camera playerCamera;

/*************  ✨ Windsurf Command ⭐  *************/
        /// <summary>
        /// Called when the script instance is being initialized.
        /// Sets up the player camera and locks the mouse cursor.
        /// </summary>
/*******  deeec770-0e91-49b3-9966-4499cd3445be  *******/
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        transform.position += move * moveSpeed * Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
