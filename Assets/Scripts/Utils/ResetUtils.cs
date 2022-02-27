using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetUtils : MonoBehaviour
{
    public GameObject loadScreen;
    public void resetGame()
    {
        SoundManager.soundsList = new List<GameObject>();
        loadScreen.SetActive(true);
        StartCoroutine(AsynchronousLoad("E5"));
    }
    public void lastScene()
    {
        SoundManager.soundsList = new List<GameObject>();
        loadScreen.SetActive(true);
        StartCoroutine(AsynchronousLoad("FinalScene"));
    }
    void UnloadAllScenesExcept(string sceneName)
    {
        int scenes = SceneManager.sceneCount;
        for (int i = 0; i < scenes; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if(scene.isLoaded)
            {
                if(scene.name != sceneName)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
    }
    IEnumerator AsynchronousLoad(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        UnloadAllScenesExcept("GameData");
        AsyncOperation ao = SceneManager.LoadSceneAsync("PlayerAndStuff", LoadSceneMode.Additive);
        ao.allowSceneActivation = false;
        AsyncOperation ao2 = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        ao2.allowSceneActivation = false;
        while (!ao2.isDone)
        {
            float progress = Mathf.Clamp01(ao2.progress / 0.9f);
            if (ao2.progress == 0.9f)
            {
                yield return new WaitForSeconds(1);
                ao.allowSceneActivation = true;
                ao2.allowSceneActivation = true;
                loadScreen.SetActive(false);
            }
            yield return null;
        }
        
        yield return null;
    }
}
