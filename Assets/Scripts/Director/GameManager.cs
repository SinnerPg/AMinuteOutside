using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static int nKill, day, dayTime;
    public GameObject pauseCanvas, hudCanvas, deathCanvas;
    public OptionsManager optionsManager;
    public InputManager inputManager;
    public PlayerManager player;
    public Light2D sunLight;
    public Text dayText;
    float timer;
    GameData gameData;
    ResetUtils utils;
    void Awake()
    {
        nKill = GameData.nKill;
        day = GameData.day;
        dayText.text = "Ritorno " + day;
        SoundManager.Initialize();
        utils = GameObject.Find("ResetUtils").GetComponent<ResetUtils>();
        if(day < 5) dayTime = 6;
        else dayTime = 21;
    }

    void Update()
    {
        if(!player.isDead && !player.isShelter)
        {
            if(inputManager.isPausing)
            {
                stopGame();
            }
            else if(!inputManager.isPausing && !TutorialManager.inTutorial)
            {
                resumeGame();
            }
        }
        if(day < 5)
        {
            timer += Time.deltaTime;
            if(timer >= 7.5f)
            {
                timer = 0;
                dayTime++;
                if(dayTime > 0 && dayTime < 13)
                {
                    float value = sunLight.intensity + 0.067f;
                    StopAllCoroutines();
                    StartCoroutine(changeIntensity(value));
                }
                else if(dayTime > 12)
                {
                    float value = sunLight.intensity - 0.067f;
                    StopAllCoroutines();
                    StartCoroutine(changeIntensity(value));
                }
                if(dayTime == 24)
                {
                    dayTime = 0;
                }
            }
        }
    }
    public void resetGame()
    {
        utils.resetGame();
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("volume", OptionsManager.volume);
        SoundManager.updateVolume();
        pauseCanvas.SetActive(false);
        hudCanvas.SetActive(true);
        inputManager.isPausing = false;
    }

    public void backMenu()
    {
        Time.timeScale = 1;
        inputManager.isPausing = false;
        SceneManager.LoadSceneAsync("MENU");
        SoundManager.soundsList = new List<GameObject>();
    }

    public void stopGame()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        hudCanvas.SetActive(false);
    }
    public void deathScreen()
    {
        hudCanvas.SetActive(false);
        deathCanvas.SetActive(true);
    }

    IEnumerator changeIntensity(float value)
    {
        float timer = 0;
        while(timer <= 1f)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0f, 1f);
            sunLight.intensity = Mathf.Lerp(sunLight.intensity, value, timer);
            yield return null;
        }
    }
}
