using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public GameObject shelterCanvas, hud, bagCanvas;
    public PlayerManager playerManager;
    public ShelterManager shelterManager;
    public void finishGame()
    {
        playerManager.isShelter = true;
        shelterCanvas.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.Shelter);
        Time.timeScale = 0;
        hud.SetActive(false);
        bagCanvas.SetActive(false);
        shelterManager.magazineBag();
        shelterManager.buildCanvas();
    }
}
