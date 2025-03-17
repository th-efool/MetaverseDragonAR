using TMPro;
using UnityEngine;
using System.Collections;

public class DragonUI : Singleton<DragonUI>
{
    GameObject InstantiateButton;
    GameObject AlignButton;
    GameObject JoystickGameObject; 
    GameObject FlyButton;
    GameObject UpArrow; 
    GameObject DownArrow; 
    GameObject TextObject; 
    TMP_Text Text;
    public Joystick Joystick { get; private set; }


    protected override void Awake()
    {
        dontDestroyOnLoad = false; 
        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f); // Wait for objects to be instantiated
        Debug.Log("Initializing Dragon UI");

        JoystickGameObject = GameObject.FindWithTag("Joystick");
        Joystick = JoystickGameObject?.GetComponent<Joystick>();  // Use null check to avoid errors
        UpArrow = GameObject.FindWithTag("UpArrow");
        DownArrow = GameObject.FindWithTag("DownArrow");
        TextObject = GameObject.FindWithTag("FlyText");
        Text = TextObject?.GetComponent<TMP_Text>();
        InstantiateButton = GameObject.FindWithTag("InstantiateButton");
        AlignButton = GameObject.FindWithTag("AlignButton");
        FlyButton = GameObject.FindWithTag("FlyLandButton");
        DragonPlacementStage(0);
    }


    public void DragonPlacementStage(int stage)
    {
        if (stage == 0)
        {
            AlignButton.SetActive(false);
            JoystickGameObject.SetActive(false);
            FlyButton.SetActive(false);
            UpArrow.gameObject.SetActive(false);
            DownArrow.gameObject.SetActive(false);
            InstantiateButton.SetActive(true);
        }

        if (stage == 1)
        {
            AlignButton.SetActive(true);
            InstantiateButton.SetActive(false);
            JoystickGameObject.SetActive(false);
        }
        if (stage == 2) 
        {
            AlignButton.SetActive(false);
            InstantiateButton.SetActive(false);
            JoystickGameObject.SetActive(true);
            FlyButton.SetActive(true);

        }
    }

    public void DragonFly(bool Fly)
    {
        if (Fly)
        {
            UpArrow.gameObject.SetActive(true);
            DownArrow.gameObject.SetActive(true);
            Text.text = "LAND";
        } else
        {
            UpArrow.gameObject.SetActive(true);
            DownArrow.gameObject.SetActive(true);
            Text.text = "FLY";
        }
    }

    
}
