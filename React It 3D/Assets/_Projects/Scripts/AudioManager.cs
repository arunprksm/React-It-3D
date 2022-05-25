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

    private static readonly string firstPlay = "FirstPlay";
    private static readonly string musicVolumePref = "musicVolumePref";
    private static readonly string sfxVolumePref = "sfxVolumePref";
    private int firstPlayInt;

    [Header("Volume")]
    [SerializeField] internal Slider musicVolumeSlider;
    [SerializeField] internal Slider sfxVolumeSlider;
    //private float musicVolume, sfxVolume;


    [SerializeField] private SoundType[] Sounds;
    private FMOD.Studio.EventInstance Music;
    private readonly FMOD.Studio.EVENT_CALLBACK eventCallback;

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
        Music.getVolume(out GameManager.Instance.musicVolume);
        VolumeControl();
    }
    private void Update()
    {
        UpdateSound();
    }
    void VolumeControl()
    {
        firstPlayInt = PlayerPrefs.GetInt(firstPlay);
        if(firstPlayInt == 0)
        {
            GameManager.Instance.musicVolume = 0.5f;
            GameManager.Instance.sfxVolume = 0.5f;
            musicVolumeSlider.value = GameManager.Instance.musicVolume;
            sfxVolumeSlider.value = GameManager.Instance.sfxVolume;
            PlayerPrefs.SetFloat(musicVolumePref, GameManager.Instance.musicVolume); //PlayerPrefs used to save Values
            PlayerPrefs.SetFloat(sfxVolumePref, GameManager.Instance.sfxVolume);
            PlayerPrefs.SetInt(firstPlay, -1);
            return;
        }
        GameManager.Instance.musicVolume = PlayerPrefs.GetFloat(musicVolumePref);
        musicVolumeSlider.value = GameManager.Instance.musicVolume;
        GameManager.Instance.sfxVolume = PlayerPrefs.GetFloat (sfxVolumePref);
        sfxVolumeSlider.value = GameManager.Instance.sfxVolume;
    }
    public void SaveSoundSetings()
    {
        PlayerPrefs.SetFloat(musicVolumePref, musicVolumeSlider.value);
        PlayerPrefs.SetFloat(sfxVolumePref, sfxVolumeSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSetings();
        }
    }

    public void UpdateSound()
    {
        Music.setVolume(musicVolumeSlider.value);
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
            var eventDescription = RuntimeManager.GetEventDescription(item.eventReference);
            eventDescription.unloadSampleData();
            eventDescription.loadSampleData();
            Music = RuntimeManager.CreateInstance(item.eventReference);
            Music.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
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