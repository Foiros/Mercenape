﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class soundManager
{
    //Written by Ossi Uusitalo, guide to it by Code Monkey, https://www.youtube.com/watch?v=QL29aTa7J5Q&t=323s
    public enum Sound
    {
        playerMove,
        playerJump,
        playerAttack,
        playerDie,
        enemyAttack,
        enemyDie,
        heal,

    }

    //This is for sound we want to play at different intervals such as footsteps
    private static Dictionary<Sound, float> soundtimerDictionary;

    public static void Initialize()
    {
        //This method is called by the Asset Manager script upon Awake()
        soundtimerDictionary = new Dictionary<Sound, float>();
        soundtimerDictionary[Sound.playerMove] = 0f;
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        AssetManager aManager = GameObject.Find("GameManager").GetComponent<AssetManager>();
        if (aManager != null) //This is a failsafe
        {
            if (CanPlaySound(sound))
            {
                //THis creates an empty game object that plays the audioclip it's been given.
                GameObject soundGameObject = new GameObject("Sound");
                soundGameObject.transform.position = position;
                AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                
                // These are to set the sound as a 3D sound.
                audioSource.maxDistance = 100f;
                audioSource.spatialBlend = 1f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.dopplerLevel = 0f;  
                audioSource.Play();
                //Once the lenght of the audioclip is done, the gameobject destroys itself from the scene.
                Object.Destroy(soundGameObject, audioSource.clip.length);

                Debug.Log(sound + " played");
            }
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        //This checks if the requested sound is in the Asset Manager array
        switch(sound)
        {
            case Sound.playerMove:
                {
                    if (soundtimerDictionary.ContainsKey(sound))
                    {
                        float lastTimeplayed = soundtimerDictionary[sound];
                        float playerMoveTimerMax = 0.5f;
                        if (lastTimeplayed + playerMoveTimerMax < Time.time)
                        {
                            soundtimerDictionary[sound] = Time.time;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    } else
                    {
                        return false;
                    }
                }
            default: { return true; }

        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        //If CanPLay SOund bool is true, it gets the named audioclip and adds it to the newly made "Sound" gameObject.
        AssetManager aManager = GameObject.Find("GameManager").GetComponent<AssetManager>();
        if (aManager != null)
        {
            foreach(AssetManager.SoundAudioClip soundAudioclip in aManager.soundAudioclipArray)
            {
                if(soundAudioclip.sound == sound)
                {
                    return soundAudioclip.audioclip;
                }
            }
            Debug.LogError("Sound not found");
            return null;
        } else
        {
            return null;
        }

                
    }
}
