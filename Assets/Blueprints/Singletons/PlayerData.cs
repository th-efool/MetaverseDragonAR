using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

public class PlayerData : Singleton<PlayerData>
{
    public string PlayerName { get; set; } = "Zeipher";
    public DragonType DragonChoice { get; private set; } = DragonType.Usurper;
    public int DragonColorChoice { get; private set; } = 2;
    Material[] Usurper;
    Material[] SoulEater;
    Material[] Nightmare;
    Material[] TerrorBringer;

    public Dictionary<DragonType, DragonColor[]> dragonColors = new Dictionary<DragonType, DragonColor[]>
    {
        { DragonType.Usurper, new DragonColor[] {DragonColor.Blue,DragonColor.Green,DragonColor.Purple,DragonColor.Red  } },
        { DragonType.SoulEater, new DragonColor[] {DragonColor.Blue,DragonColor.Green,DragonColor.Grey,DragonColor.Red  } },
        { DragonType.Nightmare, new DragonColor[] {DragonColor.Albino,DragonColor.Blue,DragonColor.DarkBlue,DragonColor.Green  } },
        { DragonType.TerrorBringer, new DragonColor[] { DragonColor.Blue, DragonColor.Green, DragonColor.Purple, DragonColor.Red } }
    };

    public Dictionary<DragonColor, UnityEngine.Color> colorMapping = new Dictionary<DragonColor, UnityEngine.Color>
    {
        { DragonColor.Blue, new UnityEngine.Color(0, 0, 1) },
        { DragonColor.Green, new UnityEngine.Color(0, 1, 0) },
        { DragonColor.Purple, new UnityEngine.Color(0.5f, 0, 0.5f) },
        { DragonColor.Red, new UnityEngine.Color(1, 0, 0) },
        { DragonColor.Grey, new UnityEngine.Color(0.5f, 0.5f, 0.5f) },
        { DragonColor.Albino, new UnityEngine.Color(1, 1, 1) },
        { DragonColor.DarkBlue, new UnityEngine.Color(0, 0, 0.5f) }
    };

    public UnityEngine.Color GetColor(DragonColor dragonColor)
    {
        return colorMapping.TryGetValue(dragonColor, out UnityEngine.Color value) ? value : UnityEngine.Color.white;
    }



    public void SetDragonChoice(DragonType Choice)
    {
        DragonChoice = Choice;
    }

    public void SetDragonColorChoice(int Choice)
    {
        DragonColorChoice = Choice;
    }

    public void ChangeMaterialBasedOnChoice(GameObject[] DragonsRef)
    {
        DragonsRef[(int)DragonChoice].GetComponentInChildren<SkinnedMeshRenderer>().material = SelectedDragonMaterial();
    }

    protected override void Awake()
    {
        dontDestroyOnLoad = true;
        base.Awake();

    }

    public Material SelectedDragonMaterial()
    {
        switch (DragonChoice)
        {
            case (DragonType.Usurper):
                return Usurper[DragonColorChoice];
            case (DragonType.SoulEater):
                return SoulEater[DragonColorChoice];
            case (DragonType.Nightmare):
                return Nightmare[DragonColorChoice];
            case (DragonType.TerrorBringer):
                return TerrorBringer[DragonColorChoice];

        }
        return Usurper[DragonColorChoice];

    }

    public void SetMaterialReference(Material[] UsuperMaterial, Material[] SoulEaterMaterial, Material[] NightmareMaterial, Material[] TerrorbringerMaterial)
    {
        Usurper = UsuperMaterial;
        SoulEater = SoulEaterMaterial;
        Nightmare = NightmareMaterial;
        TerrorBringer = TerrorbringerMaterial;  
    }
 
}
