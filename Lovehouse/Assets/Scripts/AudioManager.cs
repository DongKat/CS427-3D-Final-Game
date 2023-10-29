using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource enemySource = null, musicSource = null, itemSource = null, playerSource = null;

    public AudioMixerGroup enemyGroup, musicGroup, playerGroup, SFXGroup;

    // Player, Enemy, item pickup, light switch, ambient

    public AudioClip[] enemyClip, musicClip, itemClip, playerClip;
    // Start is called before the first frame update


    [SerializeField]
    private float enemyVolume, musicVolume, playerVolume, SFXVolume, MasterVolume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        enemyVolume = PlayerPrefs.GetFloat("MusicVolume");
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        playerVolume = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolume = PlayerPrefs.GetFloat("MusicVolume");
        MasterVolume = PlayerPrefs.GetFloat("MusicVolume");

        enemySource.volume = enemyVolume;
        musicSource.volume = musicVolume;
        playerSource.volume = playerVolume;
        itemSource.volume = SFXVolume;

        enemySource.outputAudioMixerGroup = enemyGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        itemSource.outputAudioMixerGroup = SFXGroup;

        // Init background music
        PlayMusic();

    }

    public static void changeVolume()
    {
        instance.enemyVolume = PlayerPrefs.GetFloat("MusicVolume");
        instance.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        instance.playerVolume = PlayerPrefs.GetFloat("MusicVolume");
        instance.SFXVolume = PlayerPrefs.GetFloat("MusicVolume");
        instance.MasterVolume = PlayerPrefs.GetFloat("MusicVolume");

        instance.enemySource.volume = instance.enemyVolume;
        instance.musicSource.volume = instance.musicVolume;
        instance.playerSource.volume = instance.playerVolume;
        instance.itemSource.volume = instance.SFXVolume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Player Audio ----------------------------------------------
    public static void stopAudio()
    {
        if (instance == null)
            return;
        instance.playerSource.Stop();
    }

    public static void PlayWalk()
    {
        if (instance == null)
            return;
        if (instance.playerSource.isPlaying && instance.playerSource.clip == instance.musicClip[0])
            return;
        instance.playerSource.clip = instance.playerClip[0];
        instance.playerSource.loop = true;
        instance.playerSource.Play();
    }

    public static void PlayRun()
    {
        if (instance == null)
            return;
        if (instance.playerSource.isPlaying && instance.playerSource.clip == instance.playerClip[1])
            return;
        instance.playerSource.clip = instance.playerClip[1];
        instance.playerSource.loop = true;
        instance.playerSource.Play();
    }

    public static void PlayIdle()
    {
        if (instance == null)
            return;
        if (instance.playerSource.isPlaying && instance.playerSource.clip == instance.playerClip[2])
            return;
        instance.playerSource.clip = instance.playerClip[2];
        instance.playerSource.loop = true;
        instance.playerSource.Play();
    }

    public static void PlayJump()
    {
        if (instance == null)
            return;
        if (instance.playerSource.isPlaying && instance.playerSource.clip == instance.playerClip[3])
            return;
        instance.playerSource.clip = instance.playerClip[3];
        instance.playerSource.loop = false;
        instance.playerSource.Play();
    }

    public static void PlayLand()
    {
        if (instance == null)
            return;
        if (instance.playerSource.isPlaying && instance.playerSource.clip == instance.playerClip[4])
            return;
        instance.playerSource.clip = instance.playerClip[4];
        instance.playerSource.loop = false;
        instance.playerSource.Play();
    }


    // ENEMY AUDIO ----------------------------------------------



    public static void PlayEnemyWalk()
    {
        if (instance == null)
            return;
        Debug.Log("Playing enemy walk");
        if (instance.enemySource.isPlaying && instance.enemySource.clip == instance.enemyClip[0])
            return;
        instance.enemySource.clip = instance.enemyClip[0];
        instance.enemySource.loop = true;
        instance.enemySource.Play();
    }
    public static void PlayEnemyRun()
    {
        if (instance == null)
            return;
        if (instance.enemySource.isPlaying && instance.enemySource.clip == instance.enemyClip[1])
            return;
        Debug.Log("Playing enemy run");
        instance.enemySource.clip = instance.enemyClip[1];
        instance.enemySource.loop = true;
        instance.enemySource.Play();
    }
    public static void PlayEnemyIdle()
    {
        if (instance == null)
            return;
        Debug.Log("Playing enemy idle");
        instance.enemySource.clip = instance.enemyClip[2];
        instance.enemySource.loop = true;
        instance.enemySource.Play();
    }
    public static void PlayEnemyRoar()
    {
        if (instance == null)
            return;
        if (instance.enemySource.isPlaying && instance.enemySource.clip == instance.enemyClip[3])
            return;
        Debug.Log("Playing enemy roar");
        instance.enemySource.clip = instance.enemyClip[3];
        instance.enemySource.loop = false;
        instance.enemySource.Play();
    }
    public static void PlayEnemyBite()
    {
        if (instance == null)
            return;
        Debug.Log("Playing enemy bite");
        if (instance.enemySource.isPlaying && instance.enemySource.clip == instance.enemyClip[4])
            return;
        instance.enemySource.clip = instance.enemyClip[4];
        instance.enemySource.loop = true;
        instance.enemySource.Play();
    }

    // MUSIC AUDIO ----------------------------------------------
    public static void PlayMusic()
    {
        if (instance == null)
            return;
        instance.musicSource.clip = instance.musicClip[0];
        instance.musicSource.loop = true;
        instance.musicSource.Play();
    }

    public static void PlayMusic2()
    {
        if (instance == null)
            return;
        if (instance.musicSource.isPlaying && instance.musicSource.clip == instance.musicClip[1])
            return;
        instance.musicSource.clip = instance.musicClip[1];
        instance.musicSource.loop = true;
        instance.musicSource.Play();
    }

    // ITEM AUDIO ----------------------------------------------

    public static void PlayPickup()
    {
        if (instance == null)
            return;
        instance.itemSource.clip = instance.itemClip[0];
        instance.itemSource.loop = false;
        instance.itemSource.Play();
    }
    public static void PlayLightSwitch()
    {
        if (instance == null)
            return;
        instance.itemSource.clip = instance.itemClip[1];
        instance.itemSource.loop = false;
        instance.itemSource.Play();
    }


}
