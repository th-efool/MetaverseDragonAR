using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    IADragon IADragon;
    [SerializeField] Rigidbody rb;
    [SerializeField]Joystick joystick;
    [SerializeField] Animator animator;
    int DirectionHash;


    [Header("STATIC VALUES")]
    [SerializeField] float GROUND_ACCELERATION;
    [SerializeField] float MAX_SPEED;
    [SerializeField] float HORIZONTAL_TURN_RATE;
    [SerializeField] float SENSTITVITY_HORIZONTAL_TURN = 90f;
    Vector3 MovementDirectionInput;

    #region DynamicVariables
    bool ReceivingMovementInput;

    #endregion

    #region DebugValues
    public Vector3 velocityRigidbody;
    Vector2 HorizontalInputVector;
    #endregion

    private void Awake()
    {
        IADragon = new IADragon();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        DirectionHash = Animator.StringToHash("Direction");
        MovementDirectionInput = transform.forward;
    }
    void Start()
    {
    }

    void Update()
    {
        velocityRigidbody = rb.linearVelocity;
    }


    private void OnEnable()
    {
        IADragon.Locomotion.GroundMovement.started += HandleInputDirectionMovement;
        IADragon.Locomotion.GroundMovement.performed += HandleInputDirectionMovement;
        IADragon.Locomotion.GroundMovement.canceled += HandleInputDirectionMovement;
        IADragon.Enable();
    }
    private void OnDisable()
    {
        IADragon.Disable();
    }

    void HandleInputDirectionMovement(InputAction.CallbackContext ctx)
    {
        HorizontalInputVector = ctx.ReadValue<Vector2>();
        if (HorizontalInputVector.magnitude > 0.1f)
        {
            ReceivingMovementInput = true;
            if (rb.linearVelocity.magnitude > MAX_SPEED / 20f)
            {
                MovementDirectionInput = (Quaternion.Euler(0, HorizontalInputVector.x * SENSTITVITY_HORIZONTAL_TURN, 0) * MovementDirectionInput).normalized;
            }
            if (HorizontalInputVector.y < -0.4f)
            {
                MovementDirectionInput = (Quaternion.Euler(0, HorizontalInputVector.magnitude * SENSTITVITY_HORIZONTAL_TURN*2, 0) * MovementDirectionInput).normalized;
                Debug.Log("YOU ARE PRESSING S");
            }
        }
        else { ReceivingMovementInput = false; }
    }


    private void FixedUpdate()
    {
        animator.SetFloat(DirectionHash, (rb.linearVelocity.magnitude) / MAX_SPEED);

        if (joystick.Horizontal>0.2 || joystick.Vertical > 0.2)
        {
            rb.AddForce(transform.forward * GROUND_ACCELERATION);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(((transform.right*joystick.Horizontal)+(transform.forward*joystick.Vertical)).normalized), HORIZONTAL_TURN_RATE * Time.deltaTime);
            if (rb.linearVelocity.magnitude > MAX_SPEED)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * MAX_SPEED;
            }
        }
    }
}
