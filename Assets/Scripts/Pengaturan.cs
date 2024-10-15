using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Pengaturan : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private AudioSource musicSource;
    private AudioSource sfxSource;
    void Start()
    {
        musicSource = AudioSetup.instance.mainSource;
        sfxSource = AudioSetup.instance.sfxSource;
        if(!PlayerPrefs.HasKey("musicVolume") && !PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            PlayerPrefs.SetFloat("sfxVolume", 1);
            Load();
        }
        else if(PlayerPrefs.HasKey("musicVolume") && !PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            Load();
        }
        else if (!PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void ChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value;
        Debug.Log("sfx value = " + sfxSource.volume);
        Debug.Log("music value = " + musicSource.volume);
        PlayerPrefs.SetFloat("musicVolume", musicSource.volume);
    }
    public void ChangeSfxVolume()
    {
        sfxSource.volume = sfxSlider.value;
        Debug.Log("sfx value = " + sfxSource.volume);
        Debug.Log("music value = " + musicSource.volume);
        PlayerPrefs.SetFloat("sfxVolume", sfxSource.volume);
    }
    public void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        musicSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;
    }
    public void Button_click()
    {
        AudioSetup.instance.playSfx("sfx_button");   
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
