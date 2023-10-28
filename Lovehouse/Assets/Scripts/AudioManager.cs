using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource enemySource, musicSource;

    // public AudioMixerGroup enemyGroup, musicGroup;

    // Player, Enemy, item pickup, light switch, ambient

    public AudioClip[] enemyClip, musicClip;
    // Start is called before the first frame update

    [SerializeField]
    private float enemyVolume, musicVolume;

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

        // enemySource.volume = enemyVolume;
        // musicSource.volume = musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (instance.enemySource.isPlaying && instance.enemySource.clip == instance.enemyClip[2])
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
        if (instance.enemySource.isPlaying && instance.enemySource.clip == instance.enemyClip[4])
            return;
        Debug.Log("Playing enemy bite");
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
        instance.musicSource.clip = instance.musicClip[1];
        instance.musicSource.loop = true;
        instance.musicSource.Play();
    }


}
