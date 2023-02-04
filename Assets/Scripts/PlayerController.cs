using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 100f;
    public const float RUNSPEED = 15f;
    public const float WALKSPEED = 10f;

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
    }

    void GetInputs()
    {
        move = controls.GetMovement();
        look = controls.GetLook();
        isSprint = controls.GetIsSprinting();

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
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // sprint ability
        if (isSprint)
        {
            moveSpeed = RUNSPEED;
        }
        else
        {
            moveSpeed = WALKSPEED;
        }
    }
}
