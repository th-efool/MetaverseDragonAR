using UnityEngine;

public class AR_SCENE_UIHANDLER : MonoBehaviour
{
    public void BackToMainMenu()
    {
    CustomSceneLoader.Instance.LoadScene(LevelList.MainMenu);
    }
}
