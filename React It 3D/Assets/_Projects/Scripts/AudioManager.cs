using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

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
    [Header("Volume")]
    [SerializeField] internal Slider musicVolumeSlider;
    [SerializeField] internal Slider sfxVolumeSlider;

    [SerializeField] private SoundType[] Sounds;
    private FMOD.Studio.EventInstance Music;
    private FMOD.Studio.EVENT_CALLBACK eventCallback;

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
    private void Start()
    {
        CurrentVolume();
        SetVolume();
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
        //Music.setVolume(musicVolumeSlider.value);
    }
    public void PlaySFX(Sounds sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if (item.playSound != null)
        {
            item.playSound = item.eventReference.Path;
            RuntimeManager.PlayOneShot(item.playSound);
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
            var eventDescription = FMODUnity.RuntimeManager.GetEventDescription(item.eventReference);
            eventDescription.unloadSampleData();
            eventDescription.loadSampleData();
            Music = RuntimeManager.CreateInstance(item.eventReference);
            Music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            Music.setCallback(eventCallback);
            Music.start();
            return;
        }
        Debug.LogError("Clip not found on soundType: " + sound);
    }
    public void StopSound()
    {
        Music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public EventReference eventReference;
    public string playSound = null;
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