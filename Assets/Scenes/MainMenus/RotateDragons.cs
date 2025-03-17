using UnityEngine;
using System.Collections;
using System.Timers;
using TMPro;
using UnityEngine.UI;

public class RotateDragons : MonoBehaviour
{

    [SerializeField] float FullRotationPerSecond=0.8f;
    [SerializeField] TMP_Text text;
    [SerializeField] Button SelectButton;
    [SerializeField] GameObject ColorV1;
    [SerializeField] GameObject ColorV2;
    [SerializeField] GameObject ColorV3;
    [SerializeField] GameObject ColorV4;
    int SelectionDragon =0;
    bool OngoingRotation=false;
    int NumberOfDragonTypes = System.Enum.GetValues(typeof(DragonType)).Length;

    private void Awake()
    {
        SwitchColorPalette((DragonType)SelectionDragon);
    }
    public void Rotate(float angle)
    {
        if (!OngoingRotation)
        {
            StartCoroutine(SlerpItDown(angle));
            if (SelectionDragon == (int)DragonType.Nightmare) {text.text = "LOCKED"; SelectButton.interactable = false; }
            else {text.text = "SELECT"; SelectButton.interactable = true; }
        }

    }

    IEnumerator SlerpItDown(float angle)
    {
        if (angle > 0) { SelectionDragon++; }else { SelectionDragon--; }
        if (SelectionDragon < 0 || SelectionDragon >= NumberOfDragonTypes)
        {
            SelectionDragon=((SelectionDragon % NumberOfDragonTypes) + NumberOfDragonTypes) % NumberOfDragonTypes;
        }

        OngoingRotation = true;
        float alpha = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion finalRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
        while (alpha < 1.0f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, finalRotation, alpha);
            alpha += FullRotationPerSecond * Time.deltaTime;
            yield return null;
        }
        transform.rotation = finalRotation; //for precision
        OngoingRotation = false;
        PlayerData.Instance.SetDragonChoice((DragonType)SelectionDragon);
        SwitchColorPalette((DragonType)SelectionDragon);

    }

    private void SwitchColorPalette(DragonType SelectedDragonType)
    {
        switch (PlayerData.Instance.DragonChoice)
        {
            case DragonType.Usurper:
                SetColorPalette(DragonType.Usurper);
                break;
            case DragonType.SoulEater:
                SetColorPalette(DragonType.SoulEater);
                break;
            case DragonType.Nightmare:
                SetColorPalette(DragonType.Nightmare);
                break;
            case DragonType.TerrorBringer:
                SetColorPalette(DragonType.TerrorBringer);
                break;

        }

         ;
    }

    private void SetColorPalette(DragonType dragontype)
    {
        DragonColor[] dragonColorPalette = PlayerData.Instance.dragonColors[dragontype];
        ColorV1.GetComponent<Image>().color = PlayerData.Instance.colorMapping[dragonColorPalette[0]];
        ColorV2.GetComponent<Image>().color = PlayerData.Instance.colorMapping[dragonColorPalette[1]];
        ColorV3.GetComponent<Image>().color = PlayerData.Instance.colorMapping[dragonColorPalette[2]];
        ColorV4.GetComponent<Image>().color = PlayerData.Instance.colorMapping[dragonColorPalette[3]];

    }
}

/*  transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0, angle, 0), alpha);
 * If you try put this in IEnumerator or eent update this wont work since what tranform.rotation reperesent would always get updated each time, so like whats the celling is keep getting pushed you'll get cranky results which you dont want
 * What we wanna execute is controlled rotation.
 * So we'll store value of rotation at start point and end point so we can smoothly slerp
 */