using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private InputAction Move;
    [SerializeField] private float rotatiomSpeed = -20;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private LayerMask  groundLayer;
    [SerializeField] private Vector3 pushbackForce;
    
    [SerializeField] private bool  disabled;
     private float lastDisableTime;
    [SerializeField] private float disableTime = 0.7f;
    
    private Rigidbody rb;
    public static Transform playerPos;
    private Animator anim;


    void Awake()
    {
        Move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerPos = transform;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.Linecast(transform.position, transform.position - transform.up, groundLayer);
        if (Time.timeSinceLevelLoad > lastDisableTime + disableTime)
            disabled = false;
        if (isGrounded && !disabled)
        {
            Vector2 MoveVector = Move.ReadValue<Vector2>();
            float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
            float SpeedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
            rb.AddForce(transform.forward * moveSpeed * SpeedMultiplier * Time.fixedDeltaTime);
            transform.Rotate(0, MoveVector.x * rotatiomSpeed * Time.fixedDeltaTime, 0);
        }
        anim.SetBool("grounded", isGrounded);
        anim.SetFloat("playerSpeed", rb.linearVelocity.magnitude);
    }
    
}
