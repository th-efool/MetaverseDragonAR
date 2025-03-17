using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading;

public class CustomSceneLoader : Singleton<CustomSceneLoader>
{
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

        AsyncLoadedScene.allowSceneActivation = true;

        yield return new WaitUntil(() => AsyncLoadedScene.isDone);
    }


}
