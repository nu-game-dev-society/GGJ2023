using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 100f;
    public float speedMultiplier = 1f;
    public const float JOG_SPEED_MULTIPLIER = 1.1f;
    float xRotation = 0f;

    CharacterController controller;
    ControlsManager controls;

    public Transform camera;

    [SerializeField]
    private Vector2 move;
    [SerializeField]
    private Vector2 look;
    private int health;
    private int damageRate;
    private bool isSprint;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        controls = GameManager.Instance.Controls;

        Cursor.lockState = CursorLockMode.Locked;
        xRotation = camera.localRotation.x;

        GameManager.Instance.Controls.controls.Gameplay.Sprint.performed += Sprint_performed;
        GameManager.Instance.Controls.controls.Gameplay.Sprint.canceled += Sprint_canceled;
    }

    private void Sprint_performed(InputAction.CallbackContext obj)
    {
        this.speedMultiplier = JOG_SPEED_MULTIPLIER;
    }    
    private void Sprint_canceled(InputAction.CallbackContext obj)
    {
        this.speedMultiplier = 1f;
    }

    void GetInputs()
    {
        move = controls.GetMovement();
        look = controls.GetLook();

    }

    void Update()
    {
        GetInputs();

        float mouseX = look.x * mouseSensitivity;
        float mouseY = look.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

        Vector3 moveDirection = (transform.right * move.x) + (transform.forward * move.y) + Physics.gravity;
        controller.Move(moveDirection * moveSpeed * speedMultiplier * Time.deltaTime);
    }
}
