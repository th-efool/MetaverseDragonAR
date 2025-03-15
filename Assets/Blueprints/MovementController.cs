using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    IADragon IADragon;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    int DirectionHash;

    [Header("STATIC VALUES")]
    [SerializeField] float GROUND_ACCELERATION;
    [SerializeField] float MAX_SPEED;
    [SerializeField] float HORIZONTAL_TURN_RATE;
    Vector3 MovementDirectionInput;

    #region DynamicVariables
    bool ReceivingMovementInput;
    #endregion

    #region DebugValues
    public Vector3 velocityRigidbody;
    #endregion

    private void Awake()
    {
       IADragon = new IADragon();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        DirectionHash = Animator.StringToHash("Direction");
        MovementDirectionInput=transform.forward;

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
        Vector2 LocalInputVector=ctx.ReadValue<Vector2>();
        if (LocalInputVector.magnitude > 0.1f) {
            ReceivingMovementInput = true;
            MovementDirectionInput = (Quaternion.Euler(0, LocalInputVector.x*45f, 0)*MovementDirectionInput).normalized; 
        }
        else { ReceivingMovementInput = false; }
    }


    private void FixedUpdate()
    {
        animator.SetFloat(DirectionHash, (rb.linearVelocity.magnitude) / MAX_SPEED);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MovementDirectionInput), HORIZONTAL_TURN_RATE * Time.deltaTime);

        if (ReceivingMovementInput) {
            rb.AddForce(transform.forward * GROUND_ACCELERATION); 
             if (rb.linearVelocity.magnitude > MAX_SPEED)
            {
            rb.linearVelocity = rb.linearVelocity.normalized * MAX_SPEED;
             }
        }
    }
}
