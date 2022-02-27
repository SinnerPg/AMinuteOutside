using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        Walk,
        PickUp,
        Melee,
        MeleeZombie,
        Death,
        Hurt1,
        Hurt2,
        Heal,
        Shelter,
        Dash,
        SGShot,
        SMGShot,
        BossShot,
        Pollo,
        Bandage
    }
    private static Dictionary<Sound, float> soundTimerDictionary;
    public static List<GameObject> soundsList = new List<GameObject>();
    static Transform parent;
    private static float volume;
    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.Walk] = 0f;
        soundTimerDictionary[Sound.Melee] = 0f;
        soundTimerDictionary[Sound.MeleeZombie] = 0f;
        soundTimerDictionary[Sound.BossShot] = 0f;
        parent = GameObject.Find("GameObjectContainer").transform;
        volume = PlayerPrefs.GetFloat("volume", 50);
    }
    public static void updateVolume()
    {
        volume = PlayerPrefs.GetFloat("volume", 50);
    }
    public static void PlaySound(Sound audio)
    {
        if(CanPlaySound(audio))
        {
            GameObject soundGameObject = GetAudioInList();
            if(soundGameObject)
            {
                soundGameObject.SetActive(true);
                AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
                if(audio == Sound.Walk)
                {
                    audioSource.volume = 0.2f * (volume * 0.01f);
                }
                else if(audio == Sound.SGShot)
                {
                    audioSource.volume = 0.5f * (volume * 0.01f);
                }
                else
                {
                    audioSource.volume = 1f * (volume * 0.01f);
                }
                audioSource.PlayOneShot(GetAudioClip(audio));
            }
            else
            {
                soundGameObject = new GameObject("Sound");
                soundGameObject.transform.parent = parent;
                AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                if(audio == Sound.Walk)
                {
                    audioSource.volume = 0.2f * (volume * 0.01f);
                }
                else if(audio == Sound.SGShot)
                {
                    audioSource.volume = 0.5f * (volume * 0.01f);
                }
                else
                {
                    audioSource.volume = 1f * (volume * 0.01f);
                }
                soundGameObject.AddComponent<AudioManager>();
                soundsList.Add(soundGameObject);
                audioSource.PlayOneShot(GetAudioClip(audio));
            }
        }
        
    }

    private static AudioClip GetAudioClip(Sound audio)
    {
        foreach (SoundAssets.SoundClip soundClip in SoundAssets.instance.clips)
        {
            if(soundClip.soundName == audio)
            {
                return soundClip.clip;
            }
        }
        return null;
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch(sound)
        {
            default: 
                return true;
            case Sound.Walk:
                if(soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float timerMax = .35f;
                    if(lastTimePlayed + timerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            case Sound.Melee:
                if(soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float timerMax = .5f;
                    if(lastTimePlayed + timerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            case Sound.MeleeZombie:
                if(soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float timerMax = .3f;
                    if(lastTimePlayed + timerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            case Sound.BossShot:
                if(soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float timerMax = 1f;
                    if(lastTimePlayed + timerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
        }
    } 

    private static GameObject GetAudioInList()
    {
        if(soundsList.Count > 0)
        {
            for(int i = 0; i < soundsList.Count; i++)
            {
                if(!soundsList[i].activeInHierarchy)
                {
                    return soundsList[i];
                }
            }
        }

        return null;
    }
}
