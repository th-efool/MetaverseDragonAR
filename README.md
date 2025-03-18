Throughout the project, my aim has been to make the project as easy to expand as it can - extra scenes, extra dragons, extra skins, more sound effects for same thing.

i have attempted to build different systems and organize logic in dpecific way to acheive that goal

# Core Systems Overview
## Player Data Storage
An singleton script runs in background in which variables such as `PlayerName` `DragonChoice`  `DragonColorChoice` are stored and updated using getters and setters. 
It also hold methods for changing material of dragon prefabs.

  ``` c#
  public void ChangeMaterialBasedOnChoice(GameObject[] DragonsRef)
  {
      DragonsRef[(int)DragonChoice].GetComponentInChildren<SkinnedMeshRenderer>().material = SelectedDragonMaterial();
  }
  ```
## Enums 
Level Names are stored within an enum `LevelList` which when needed we can just access via `LevelName._levelName_.ToString()`

`DragonTypes` are also stored within an enum, so for easy expansion of system with addition of more dragons. 

## Scene Loader
A singleton SceneLoader script is used to load scenes asynchorniously, and until the level is loaded atleast 90%, we display an LoadingScreen.

### Dragon Selection Scene
The dragon selection scene contains four dragon prefabs that randomly play animation, clicking "<" or ">" button runs an script that
1. Updates SelectedDragon.
```c#
        if (SelectionDragon < 0 || SelectionDragon >= NumberOfDragonTypes)
        {
            SelectionDragon=((SelectionDragon % NumberOfDragonTypes) + NumberOfDragonTypes) % NumberOfDragonTypes;
        }
```
2. Rotates the parent object holding 4 dragon (with an slerp function) in scene so the dragon at 90* Clockwise, Anticlockwise is shown.
3. Updates the color pallete that is available for that dragon (based on available textures) - `PlayerData` singleton is responsible for proving the problem pallete for the CurrentDragon.

 Upon selecting an skin color from palete, an method in `PlayerData` (singelton)
 which take dragon prefabs as input and updates the skin by accessing the skinmeshrenderer component and setting material through that.

 # Rest of documatation Work In Progress 
