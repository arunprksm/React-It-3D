using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundType[] Sounds;
    //public EventReference eventReference;
    //[SerializeField] private string sfxEvent = null;
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
    LevelSelection,
    Music,
    PlayerMove,
    PlayerDeath
    //EnemyDeath,
    //GameMusic_scene1,
}