using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class StartGameManager : MonoBehaviour
{
    public GameData gameData;
    public GameObject fadeBlack, creditsCanvas;
    public List<Button> buttons;
    void Awake()
    {
        SceneManager.LoadSceneAsync("GameData", LoadSceneMode.Additive);
        string dataPath = Application.dataPath + "/savedata.amo";
        if(File.Exists(dataPath))
        {
            buttons[0].gameObject.SetActive(true);
        }
    }
    public void startGame()
    {
        foreach(Button button in buttons)
        {
            button.enabled = false;
        }
        if(!gameData)
        {
            gameData = GameObject.Find("GameData").GetComponent<GameData>();
        }
        gameData.initializeGame();
        fadeBlack.SetActive(true);
        StartCoroutine(AsynchronousLoad("E5"));
    }
    public void loadGame()
    {
        foreach(Button button in buttons)
        {
            button.enabled = false;
        }
        if(!gameData)
        {
            gameData = GameObject.Find("GameData").GetComponent<GameData>();
        }
        gameData.load();
        fadeBlack.SetActive(true);
        if(GameData.day < 5) StartCoroutine(AsynchronousLoad("E5"));
        else StartCoroutine(AsynchronousLoad("FinalScene"));
    }
    public void openCredits()
    {
        foreach(Button button in buttons)
        {
            button.enabled = false;
        }
        creditsCanvas.SetActive(true);
    }
     public void closeCredits()
    {
        foreach(Button button in buttons)
        {
            button.enabled = true;
        }
        creditsCanvas.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator AsynchronousLoad (string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
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
                SceneManager.UnloadSceneAsync("MENU");
            }
            yield return null;
        }
        
        yield return null;
    }
}
