using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading;

public class SceneLoader : Singleton<SceneLoader>
{
    public void LoadScene(LevelList Level)
    {
        SceneManager.LoadSceneAsync("LoadingScene");
        StartCoroutine(AsyncLoad(Level));
    }

    IEnumerator AsyncLoad(LevelList Level)
    {
        string SceneName = Level.ToString();
        var AsyncLoadedScene = SceneManager.LoadSceneAsync(SceneName);

        AsyncLoadedScene.allowSceneActivation = false;

        yield return new WaitUntil(() => AsyncLoadedScene.progress >= 0.9f);

        AsyncLoadedScene.allowSceneActivation = true;

        yield return new WaitUntil(() => AsyncLoadedScene.isDone);
    }


}
