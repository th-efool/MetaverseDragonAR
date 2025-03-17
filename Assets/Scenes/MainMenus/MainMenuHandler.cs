using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public void StartGameLoadLevel()
    {
        CustomSceneLoader.Instance.LoadScene(LevelList.SelectionScreen);
    }
}
