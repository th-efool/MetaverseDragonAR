using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


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
    GameObject CenterDot;
    GameObject DebugAutoPlaceButton;
    GameObject FlameThrowerButton;
    GameObject FireballButton;
    public Joystick Joystick { get; private set; }


    protected override void Awake()
    {
        dontDestroyOnLoad = false; 
        base.Awake();

        JoystickGameObject = GameObject.FindWithTag("Joystick");
        Joystick = JoystickGameObject.GetComponent<Joystick>();
        UpArrow = GameObject.FindWithTag("UpArrow");
        DownArrow = GameObject.FindWithTag("DownArrow");
        TextObject = GameObject.FindWithTag("FlyText");
        Text = TextObject?.GetComponent<TMP_Text>();
        InstantiateButton = GameObject.FindWithTag("InstantiateButton");
        AlignButton = GameObject.FindWithTag("AlignButton");
        FlyButton = GameObject.FindWithTag("FlyLandButton");
        DragonPlacementStage(0);
        CenterDot= GameObject.FindWithTag("CenterDot");
        DebugAutoPlaceButton = GameObject.FindWithTag("DebugAutoPlaceButton");
        FlameThrowerButton = GameObject.FindWithTag("FlameThrowerButton");
        FireballButton = GameObject.FindWithTag("FireballButton");

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
            CenterDot.SetActive(false);
            DebugAutoPlaceButton.SetActive(false);
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
            UpArrow.gameObject.SetActive(false);
            DownArrow.gameObject.SetActive(false);
            Text.text = "FLY";
        }

    }

    public void FlameThrowerUI(bool on)
    {
        FlameThrowerButton.GetComponent<Button>().interactable = on;
    }

}

    

