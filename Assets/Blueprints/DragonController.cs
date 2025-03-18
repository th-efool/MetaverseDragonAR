using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System.Collections;

public class DragonController : MonoBehaviour
{
    IADragon IADragon;
    Rigidbody rb;
    Animator animator;
    int DirectionHash;
    int TakeOffHash;
    bool InAir;
    Vector3 AntiGravitationalForce;
    [SerializeField] GameObject Flamethrower;
    [SerializeField] float MAX_SPEED = 10f;
    [SerializeField] float FLIGHT_SPEED_MULTIPLIYER = 8f;
    [SerializeField] float GROUND_ACCELERATION = 15f;
    [SerializeField] float FLIGHT_ACCELERATION_MULTIPLIYER = 5f;
    [SerializeField] float ANTIGRAVITY_MULTIPLIER = 1.05f;
    Joystick joystick;
    public Vector3 PureHorizontalVelocity;
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform ProjectileStartPoint;
    [SerializeField] float ProjectileSpeed= 250f;
    bool flamethrowerOn;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        DirectionHash = Animator.StringToHash("Direction");
        TakeOffHash = Animator.StringToHash("TakeOff");
        IADragon = new IADragon();
        AntiGravitationalForce = new Vector3(0, (float)(-Physics.gravity.y), 0);
        joystick = DragonUI.Instance.Joystick;
        FlameThrowerEnable(false);
    }

    private void FixedUpdate()
    {
        PureHorizontalVelocity = rb.linearVelocity;
        PureHorizontalVelocity.y = 0;
        Vector3 MovementDirection = new(joystick.Horizontal, 0, joystick.Vertical);
        if (PureHorizontalVelocity.magnitude < MAX_SPEED) { rb.AddForce(MovementDirection * GROUND_ACCELERATION, ForceMode.Acceleration); }
        if (joystick.Vertical > 0.2 || joystick.Horizontal > 0.2 || (joystick.Vertical < -0.2) || joystick.Horizontal < -0.2)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MovementDirection), 0.05f);
        }
        if (InAir) { StayAfloat(); }
        animator.SetFloat(DirectionHash, (PureHorizontalVelocity.magnitude) / MAX_SPEED);
    }

    private void OnEnable()
    {

        IADragon.Enable();
        IADragon.Locomotion.Fly.started += TakeFlight;
        IADragon.Locomotion.FlyUpDown.performed += AltitudeChange;
        IADragon.Locomotion.FlyUpDown.canceled += AltitudeChange;
        IADragon.Locomotion.FlameBreath.started += FlameThrowerAttack;
        IADragon.Locomotion.Fireball.started += ShootProjectile;


    }
    private void OnDisable()
    {
        IADragon.Disable();
        IADragon.Locomotion.Fly.started -= TakeFlight;
        IADragon.Locomotion.FlyUpDown.performed -= AltitudeChange;
        IADragon.Locomotion.FlyUpDown.canceled -= AltitudeChange;
        IADragon.Locomotion.FlameBreath.started -= FlameThrowerAttack;
        IADragon.Locomotion.Fireball.started -= ShootProjectile;


    }

    void FlameThrowerEnable(bool enable)
    {if (enable) { Flamethrower.gameObject.SetActive(true); } else { Flamethrower.gameObject.SetActive(false); }
    }

    void TakeFlight(InputAction.CallbackContext callbackContext)
    {
        if (InAir) { ExitFlight(); 
        }
        else
        {
            InAir = true; animator.SetBool(TakeOffHash, true);
            MAX_SPEED = MAX_SPEED * FLIGHT_SPEED_MULTIPLIYER;
            GROUND_ACCELERATION = GROUND_ACCELERATION * FLIGHT_ACCELERATION_MULTIPLIYER;
            rb.linearDamping = 1.0f;
            DragonUI.Instance.DragonFly(true);
        }
    }

    void ExitFlight()
    {
        InAir = false; animator.SetBool(TakeOffHash, false);
        MAX_SPEED = MAX_SPEED / FLIGHT_SPEED_MULTIPLIYER;
        GROUND_ACCELERATION = GROUND_ACCELERATION / FLIGHT_ACCELERATION_MULTIPLIYER;
        rb.linearDamping = 0;
        DragonUI.Instance.DragonFly(false);


    }
    void StayAfloat() { rb.AddForce(AntiGravitationalForce, ForceMode.Acceleration); }

    void AltitudeChange(InputAction.CallbackContext ctx)
    {
        float upDownValue = ctx.ReadValue<float>();
        if (ctx.canceled) { AntiGravitationalForce = new Vector3(0, (float)(-Physics.gravity.y), 0); Debug.Log("BOOOOOO"); }
        else if (upDownValue > 0)
        {
            AntiGravitationalForce = new Vector3(0, (float)(-Physics.gravity.y * ANTIGRAVITY_MULTIPLIER), 0);
            Debug.Log("BRO IS GOING UP");
        }
        else if (upDownValue < 0)
        {
            AntiGravitationalForce = new Vector3(0, (float)((-Physics.gravity.y)+((Physics.gravity.y) * ANTIGRAVITY_MULTIPLIER/1.1)), 0);
            Debug.Log("DOWN IS THE WAY");

        }
    }

    void ShootProjectile(InputAction.CallbackContext ctx)
    {
        //Raycast to get the destination point
        
        Vector3 destination;
        Ray ray = new Ray(ProjectileStartPoint.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            destination = hitInfo.point;
        } else
        {
            destination = ray.GetPoint(400);
        }

        //Instantiate Projectile
        GameObject projectile = Instantiate(Projectile, ProjectileStartPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().linearVelocity = ((destination - ProjectileStartPoint.position).normalized*ProjectileSpeed);


    }

    void FlameThrowerAttack(InputAction.CallbackContext ctx)
    {
        if (!flamethrowerOn) { StartCoroutine(FlameThrowerAttack()); }
    }

    IEnumerator FlameThrowerAttack()
    {
        FlameThrowerEnable(true);
        flamethrowerOn = true;
        DragonUI.Instance.FlameThrowerUI(false);
        yield return new WaitForSeconds(3);
        FlameThrowerEnable(false);
        flamethrowerOn = false;
        DragonUI.Instance.FlameThrowerUI(true);


    }
}


