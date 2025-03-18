using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading;
using TMPro;

public class CustomSceneLoader : Singleton<CustomSceneLoader>
{
    int sceneloaded;
    TMP_Text ProgressText;
    public void LoadScene(LevelList SelectedLevel)
    {
        SceneManager.LoadSceneAsync("LoadingScene",LoadSceneMode.Single);

        StartCoroutine(AsyncLoad(SelectedLevel));
    }

    IEnumerator AsyncLoad(LevelList SelectedLevel)
    {
        string SceneName = SelectedLevel.ToString();
        var AsyncLoadedScene = SceneManager.LoadSceneAsync(SceneName,LoadSceneMode.Single);

        AsyncLoadedScene.allowSceneActivation = false;

        yield return new WaitUntil(() => AsyncLoadedScene.progress >= 0.9f);
        ProgressText = null;
        AsyncLoadedScene.allowSceneActivation = true;

        yield return new WaitUntil(() => AsyncLoadedScene.isDone);
    }


}
