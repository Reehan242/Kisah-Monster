using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credit : MonoBehaviour
{
    void Start()
    {
        AudioSetup.instance.playMusic();
    }
    public void ToMenu()
    {
        AudioSetup.instance.playSfx("sfx_button");
        SceneManager.LoadScene("MainMenu");
    }
}
