using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonTransisi : MonoBehaviour
{
    public void transisi()
    {
        AudioSetup.instance.playSfx("sfx_button");
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (gameObject.name == currentSceneName)
        {
            GameObject CanvasNarasi = GameObject.Find("CanvasNarasi");
            CanvasNarasi.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(gameObject.name);
        }
    }
}
