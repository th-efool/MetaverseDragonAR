using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] LevelList SelectedLevel;
    public void LoadLevel()
    {
        CustomSceneLoader.Instance.LoadScene(SelectedLevel);
    }
}
