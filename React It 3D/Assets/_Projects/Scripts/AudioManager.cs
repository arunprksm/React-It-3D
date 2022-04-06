using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("VolumeControl")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    private float musicVolume;
    private float sfxVolume;

    [SerializeField] private SoundType[] Sounds;
    private FMOD.Studio.EventInstance playMusic;
    private FMOD.Studio.EventInstance playSfx;
    private FMOD.Studio.EVENT_CALLBACK eventCallback;
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
            return;
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        CurrentVolume();
    }
    private void Update()
    {
        SetVolume();
    }
    private void CurrentVolume()
    {

        musicVolumeSlider.value = GameManager.Instance.currentMusicVolume;
        sfxVolumeSlider.value = GameManager.Instance.currentSfxVolume;
        
    }
    private void SetVolume()
    {
        GameManager.Instance.currentMusicVolume = musicVolumeSlider.value;
        GameManager.Instance.currentSfxVolume = sfxVolumeSlider.value;
        playMusic.setVolume(GameManager.Instance.musicVolume);
        
        
    }
    public void PlaySFX(Sounds sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if (item.eventReference.Path != null)
        {
            RuntimeManager.PlayOneShot(item.eventReference.Path);
            return;
        }
        Debug.LogError("Clip not found on soundType: " + sound);
    }
    public void PlayMusic(Sounds sound, GameObject gameObject)
    {
        StopSound();
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if (item.eventReference.Path != null)
        {
            var eventDescription = RuntimeManager.GetEventDescription(item.eventReference);
            eventDescription.unloadSampleData();
            eventDescription.loadSampleData();
            var eventInstance = RuntimeManager.CreateInstance(item.eventReference);
            playMusic = eventInstance;
            playMusic.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            playMusic.setCallback(eventCallback);
            playMusic.start();
            return;
        }
        Debug.LogError("Clip not found on soundType: " + sound);
    }
    public void StopSound()
    {
        playMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public EventReference eventReference;
}
public enum Sounds
{
    ButtonClick,
    ButtonBack,
    LevelSelection,
    Music,
    PlayerMove,
    PlayerDeath,
    Scene01,
    //EnemyDeath,
    //GameMusic_scene1,
}