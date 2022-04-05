using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private SoundType[] Sounds;
    private static FMOD.Studio.EventInstance Music;
    //[SerializeField] private StudioEventEmitter studioEventEmitter;

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
        //studioEventEmitter = GetComponent<StudioEventEmitter>();
    }
    public void PlaySFX(Sounds sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if (item.sfxEvent != null)
        {
            item.sfxEvent = item.eventReference.Path;
            RuntimeManager.PlayOneShot(item.sfxEvent);
            return;
        }
        Debug.LogError("Clip not found on soundType: " + sound);
    }
    public void PlayMusic(Sounds sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if (item.eventReference.Path != null)
        {
            item.sfxEvent = item.eventReference.Path;
            Music = RuntimeManager.CreateInstance(item.sfxEvent);
            Music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform));
            Music.start();
            Music.release();
            return;
        }
        Debug.LogError("Clip not found on soundType: " + sound);
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public EventReference eventReference;
    public string sfxEvent = null;
}
public enum Sounds
{
    ButtonClick,
    ButtonBack,
    LevelSelection,
    Music,
    PlayerMove,
    PlayerDeath
    //EnemyDeath,
    //GameMusic_scene1,
}