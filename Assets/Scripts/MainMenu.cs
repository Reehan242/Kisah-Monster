using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject canvasNarasi;
    public void Start()
    {
        AudioSetup.instance.playMusic();
        GameObject btn_continue = GameObject.Find("btn_continue");        
        if (SaveLoadManager.Instance.SaveFileExists())
        {
            btn_continue.GetComponent<Button>().interactable = true;
        }
        else
        {
            btn_continue.GetComponent<Button>().interactable = false;
        }
    }
    public void NewGame()
    {
        AudioSetup.instance.playSfx("sfx_button");
        // Inisialisasi data permainan baru
        // Hapus save file jika ada, untuk memulai permainan baru
        if (SaveLoadManager.Instance.SaveFileExists())
        {
            File.Delete(Path.Combine(Application.persistentDataPath, "savefile.json"));
        }
        PlayerData playerData = new PlayerData();
        SaveLoadManager.Instance.SaveGame(playerData);
        Debug.Log("Ini ngulang / newgame");
        canvasNarasi.SetActive(true);
        
    }
    public void LoadGame()
    {
        AudioSetup.instance.playSfx("sfx_button");
        // Cek apakah save file ada
        if (SaveLoadManager.Instance.SaveFileExists())
        {
            canvasNarasi.SetActive(true);
        }
        else
        {
            Debug.LogError("No save file found!");
        }
    }
    public void ExitGame()
    {
        AudioSetup.instance.playSfx("sfx_button");
        Debug.Log("Game is exiting...");
        Application.Quit();
    }
    public void ToCredit()
    {
        AudioSetup.instance.playSfx("sfx_button");
        SceneManager.LoadScene("Credit");
    }
}
