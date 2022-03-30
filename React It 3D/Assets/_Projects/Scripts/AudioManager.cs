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
        if (item.sfxEvent != null)
        {
            item.sfxEvent = item.eventReference.Path;
            RuntimeManager.PlayOneShot(item.sfxEvent);
        }
        else
        {
            Debug.LogError("Clip not found on soundType: " + sound);
        }
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