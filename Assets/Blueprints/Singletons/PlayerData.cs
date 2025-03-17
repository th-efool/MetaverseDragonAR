using UnityEngine;
using System.Collections;

public class PlayerData : Singleton<DragonUI>
{
    public string PlayerName { get; set; }
    public DragonType DragonChoice { get; private set; }
    public DragonColor DragonColorChoice { get; private set; }

    public void SetDragonChoice(DragonType Choice)
    {
        DragonChoice = Choice;
    }

    public void SetDragonColorChoice(DragonColor Choice)
    {
        DragonColorChoice = Choice;
    }


    protected override void Awake()
    {
        dontDestroyOnLoad = true;
        base.Awake();

    }

}
