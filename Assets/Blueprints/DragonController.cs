using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragonController : MonoBehaviour
{
    IADragon IADragon;
    [SerializeField] Joystick joystick;
    Rigidbody rb;
    Animator animator;
    int DirectionHash;
    int TakeOffHash;
    bool InAir;
    Vector3 AntiGravitationalForce;
    [SerializeField] float MAX_SPEED =10f;
    [SerializeField] float FLIGHT_SPEED_MULTIPLIYER = 8f;
    [SerializeField] float GROUND_ACCELERATION = 15f;
    [SerializeField] float FLIGHT_ACCELERATION_MULTIPLIYER = 5f;    
    [SerializeField] float ANTIGRAVITY_MULTIPLIER = 1.02f;



    public Vector3 PureHorizontalVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        DirectionHash = Animator.StringToHash("Direction");
        TakeOffHash = Animator.StringToHash("TakeOff");
        IADragon = new IADragon();
        AntiGravitationalForce = new Vector3(0, (float)(-Physics.gravity.y* ANTIGRAVITY_MULTIPLIER),0);


    }
    private void FixedUpdate()
    {
        PureHorizontalVelocity = rb.linearVelocity;
        PureHorizontalVelocity.y = 0;
        Vector3 MovementDirection = new(joystick.Horizontal, 0, joystick.Vertical);
        if (PureHorizontalVelocity.magnitude < MAX_SPEED) {rb.AddForce(MovementDirection * GROUND_ACCELERATION, ForceMode.Acceleration); }
        if (joystick.Vertical >0.2 || joystick.Horizontal > 0.2 || (joystick.Vertical < -0.2) || joystick.Horizontal < -0.2) { 
        transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MovementDirection), 0.05f );}
        if (InAir) { StayAfloat(); }
        animator.SetFloat(DirectionHash, (PureHorizontalVelocity.magnitude) / MAX_SPEED);

    }

    private void OnEnable()
    {
        IADragon.Enable();
        IADragon.Locomotion.Fly.started += TakeFlight;
    }

    void TakeFlight(InputAction.CallbackContext callbackContext){
    if (InAir) {ExitFlight();}
    else { InAir = true; animator.SetBool(TakeOffHash, true); 
        MAX_SPEED = MAX_SPEED * FLIGHT_SPEED_MULTIPLIYER; 
        GROUND_ACCELERATION = GROUND_ACCELERATION * FLIGHT_ACCELERATION_MULTIPLIYER;
            rb.linearDamping = 1.0f;
        }
    }

    void ExitFlight()
    {
        InAir = false; animator.SetBool(TakeOffHash, false);
        MAX_SPEED = MAX_SPEED / FLIGHT_SPEED_MULTIPLIYER;
        GROUND_ACCELERATION = GROUND_ACCELERATION / FLIGHT_ACCELERATION_MULTIPLIYER;
        rb.linearDamping = 0;

    }
    void StayAfloat() { rb.AddForce(AntiGravitationalForce,ForceMode.Acceleration); }
} 

