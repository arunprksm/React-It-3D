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
    //internal FMOD.Studio.EventInstance Music;

    private FMOD.Studio.EVENT_CALLBACK eventCallback;
    private FMOD.Studio.EventInstance Music;

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
    //public void PlayMusic(Sounds sound, GameObject gameObject)
    //{
    //    SoundType item = Array.Find(Sounds, i => i.soundType == sound);
    //    if (item.eventReference.Path != null)
    //    {
    //        //item.playSound = item.eventReference.Path;
    //        FMOD.GUID guid = item.eventReference.Guid;
    //        Music = RuntimeManager.CreateInstance(guid);
    //        Music.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
    //        Music.start();
    //        Music.release();
    //        return;
    //    }
    //    Debug.LogError("Clip not found on soundType: " + sound);
    //}
    public void PlayMusic(Sounds sound, GameObject gameObject)
    {
        StopSound();
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if (item.eventReference.Path != null)
        {
            var eventDescription = FMODUnity.RuntimeManager.GetEventDescription(item.eventReference);
            eventDescription.unloadSampleData();
            eventDescription.loadSampleData();
            var eventInstance = FMODUnity.RuntimeManager.CreateInstance(item.eventReference);
            Music = eventInstance;
            eventInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            eventInstance.setCallback(eventCallback);
            eventInstance.start();
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