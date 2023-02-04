using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 100f;

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
    }
}
