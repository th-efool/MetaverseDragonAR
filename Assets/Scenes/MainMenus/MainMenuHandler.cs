using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public void StartGameLoadLevel()
    {
        SceneLoader.Instance.LoadScene(LevelList.AR_SCENE);
    }
}
