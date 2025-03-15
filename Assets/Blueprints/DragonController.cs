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
    [SerializeField] float MAX_SPEED =10f;
    [SerializeField] float FLIGHT_SPEED_MULTIPLIYER = 8f;
    [SerializeField] float GROUND_ACCELERATION = 15f;
    [SerializeField] float FLIGHT_ACCELERATION_MULTIPLIYER = 5f;



    public float debugVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        DirectionHash = Animator.StringToHash("Direction");
        TakeOffHash = Animator.StringToHash("TakeOff");
        IADragon = new IADragon();


    }
    private void FixedUpdate()
    {
        debugVelocity = rb.linearVelocity.magnitude;
        animator.SetFloat(DirectionHash, (rb.linearVelocity.magnitude) / MAX_SPEED); 
        Vector3 MovementDirection = new(joystick.Horizontal, 0, joystick.Vertical);
        if (rb.linearVelocity.magnitude < MAX_SPEED) {rb.AddForce(MovementDirection * GROUND_ACCELERATION, ForceMode.Acceleration); }
        if (joystick.Vertical >0.2 || joystick.Horizontal > 0.2 || (joystick.Vertical < -0.2) || joystick.Horizontal < -0.2) { 
        transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MovementDirection), 0.05f );}
    }

    private void OnEnable()
    {
        IADragon.Enable();
        IADragon.Locomotion.Fly.started += ctx => { if (InAir) { InAir = false; animator.SetBool(TakeOffHash, false); MAX_SPEED = MAX_SPEED/FLIGHT_SPEED_MULTIPLIYER; GROUND_ACCELERATION = GROUND_ACCELERATION/FLIGHT_ACCELERATION_MULTIPLIYER; } else { InAir = true; animator.SetBool(TakeOffHash, true); MAX_SPEED = MAX_SPEED * FLIGHT_SPEED_MULTIPLIYER; GROUND_ACCELERATION = GROUND_ACCELERATION * FLIGHT_ACCELERATION_MULTIPLIYER; } };
    }
} 