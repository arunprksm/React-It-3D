using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    [SerializeField] internal float musicVolume = 0.5f;
    [SerializeField] internal float sfxVolume = 0.5f;
    [SerializeField] internal float currentMusicVolume;
    [SerializeField] internal float currentSfxVolume;

    private void Awake()
    {
        InitializeOnAwake();
    }
    private void InitializeOnAwake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //currentMusicVolume = musicVolume;
            //currentSfxVolume = sfxVolume;
            return;
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        musicVolume = currentMusicVolume;
        sfxVolume = currentSfxVolume;
    }
}
