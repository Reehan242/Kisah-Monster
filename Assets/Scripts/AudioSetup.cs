using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSetup : MonoBehaviour
{
    public static AudioSetup instance;
    [SerializeField] private AudioTrack[] tracks;
    public AudioSource mainSource;
    public AudioSource sfxSource;
    public string trackname;
    public string sfxname; 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this);
        AudioUtility.SetAudioSource(mainSource);
        AudioUtility.SetSFXSource(sfxSource);

        foreach (var item in tracks)
        {
            AudioUtility.AddTrack(item.trackTitle, item.trackClip);
        }
        trackname = SceneManager.GetActiveScene().name;

        AudioUtility.PlayTrack(trackname);
    }

    public void playMusic()
    {
        trackname = SceneManager.GetActiveScene().name;

        AudioUtility.PlayTrack(trackname);
    }
    public void playSpecificMusic(string name)
    {
        trackname = name;
        AudioUtility.PlayTrack(trackname);
    }

    public void playSfx(string sfx )
    {
        sfxname = sfx;
        AudioUtility.PlaySFX(sfxname);
    }
    public void stopMusic()
    {
        AudioUtility.StopMusic();
    }  
}
