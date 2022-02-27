using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> tutorials;
    public GameObject canvas;
    public InputManager inputManager;
    public RectTransform bag;
    bool hasPlayed;
    int index = 0;
    float timer;
    public static bool inTutorial; //booleana per fermare il game manager dal resettare il time scale
    void Start()
    {
        hasPlayed = GameData.hasPlayed;
    }
    void Update()
    {
        if(!hasPlayed)
        {
            timer += Time.deltaTime;
            if(timer >= 1)
            {
                inTutorial = true;
                Time.timeScale = 0;
                inputManager.enabled = false;
                canvas.SetActive(true);
                tutorials[index].SetActive(true);
                if(index == 7)
                {
                    bag.anchoredPosition = new Vector2(0,0);
                }
                else
                {
                    bag.anchoredPosition = new Vector2(-910, 0);
                }
                if(Input.GetMouseButtonDown(0))
                {
                    tutorials[index].SetActive(false);
                    index++;
                }
            }

            if(index == tutorials.Count)
            {
                hasPlayed = true;
                canvas.SetActive(false);
                inTutorial = false;
                Time.timeScale = 1;
                inputManager.enabled = true;
            }

        }
    }
}
