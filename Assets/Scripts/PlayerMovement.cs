using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector2 currentDirection = Vector2.zero;
    private Vector2 currentAcceleration = Vector2.zero;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private UnityEvent<Vector2> onAccelerationChange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Move()
    {
        rb.AddForce(new Vector3(moveInput.x, 0, moveInput.y) * acceleration * Time.fixedDeltaTime, ForceMode.Acceleration);

        if (rb.linearVelocity.x > maxSpeed)
            rb.linearVelocity = new Vector3(maxSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        if (rb.linearVelocity.x < -maxSpeed)
            rb.linearVelocity = new Vector3(-maxSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        if (rb.linearVelocity.z > maxSpeed)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, maxSpeed);
        if (rb.linearVelocity.z < -maxSpeed)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, -maxSpeed);

        Vector2 newDirection = new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).normalized;

        if (currentDirection != newDirection)
        {
            currentDirection = newDirection;
        }

        if (currentAcceleration != moveInput)
        {
            onAccelerationChange?.Invoke(moveInput);
            currentAcceleration = moveInput;
        }
    }

    public void OnCollectableCollected(GameObject collectable)
    {
        
    }
}
