using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class SelectionUITouchInput : MonoBehaviour
{
    [SerializeField] GameObject ColorV1;
    [SerializeField] GameObject ColorV2;
    [SerializeField] GameObject ColorV3;
    [SerializeField] GameObject ColorV4;
    [SerializeField] GameObject Select;
    [SerializeField] GameObject[] DragonsRef;
    [SerializeField] Material[] Usurper;
    [SerializeField] Material[] SoulEater;
    [SerializeField] Material[] Nightmare;
    [SerializeField] Material[] TerrorBringer;


    private void Awake()
    {
        PlayerData.Instance.SetMaterialReference(Usurper, SoulEater, Nightmare, TerrorBringer);
    }

    public void SetDragonColorChoice(int choice)
    {
        PlayerData.Instance.SetDragonColorChoice(choice);
        PlayerData.Instance.ChangeMaterialBasedOnChoice(DragonsRef);
        
    }
}

    // We'll implement the system of Implementing what color dragon is shown to have,
    // and setting dragon color value in player data through here


