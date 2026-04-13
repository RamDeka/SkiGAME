using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction Move;
    [SerializeField] private float rotatiomSpeed = -20;
    [SerializeField] private float moveSpeed = 10;
    private Rigidbody rb;

    void Awake()
    {
        Move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 MoveVector = Move.ReadValue<Vector2>();
        float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
        float SpeedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
        rb.AddForce(transform.forward * moveSpeed * SpeedMultiplier * Time.fixedDeltaTime);
        transform.Rotate(0, MoveVector.x * rotatiomSpeed *Time.fixedDeltaTime, 0);
        
    }
}
