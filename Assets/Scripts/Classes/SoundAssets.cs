using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets m_instance = null;
    public static SoundAssets instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType(typeof(SoundAssets)) as SoundAssets;
            }
 
            return m_instance;
        }
    }
 
    void OnApplicationQuit()
    {
        m_instance = null;
    }
    
    [System.Serializable]
    public class SoundClip {
        public SoundManager.Sound soundName;
        public AudioClip clip;
    }
    public SoundClip[] clips;
}
