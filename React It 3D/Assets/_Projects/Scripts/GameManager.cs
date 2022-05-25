using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    [SerializeField] internal float musicVolume;
    [SerializeField] internal float sfxVolume;
    [SerializeField] internal float currentMusicVolume;
    [SerializeField] internal float currentSfxVolume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
