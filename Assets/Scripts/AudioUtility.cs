using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioUtility
{
    public static AudioSource audioSource;
    public static AudioSource SFXSource;
    public static Dictionary<string, AudioClip> tracks = new Dictionary<string, AudioClip>();
    public static void SetAudioSource(AudioSource source)
    {
        audioSource = source;
    }
    public static void SetSFXSource(AudioSource source)
    {
        SFXSource = source;
    }
    public static void AddTrack(string trackTitle,AudioClip trackClip)
    {
        tracks.Add(trackTitle, trackClip);
    }
    public static void PlayTrack(string trackTitle)
    {
        if (tracks.ContainsKey(trackTitle))
        {
            audioSource.clip = tracks[trackTitle];
            audioSource.Play();
        }
        else
        {
            Debug.Log($"Track: {trackTitle} does not exist");
        }
    }
    public static void PlaySFX(string trackTitle)
    {
        if (tracks.ContainsKey(trackTitle))
        {
            SFXSource.clip = tracks[trackTitle];
            SFXSource.Play();
        }
        else
        {
            Debug.Log($"SFX: {trackTitle} does not exist");
        }
    }
    public static void StopMusic()
    {
        audioSource.Stop();
    }
}
