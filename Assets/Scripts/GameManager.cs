using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;
    private PlayerData playerData;
    [SerializeField] private GameObject canvasUI;
    [SerializeField] private GameObject canvasTas;
    [SerializeField] private GameObject panelDialog;
    [SerializeField] private GameObject btn_book1;
    [SerializeField] private GameObject btn_book2;
    [SerializeField] private GameObject btn_book3;

    [SerializeField] private GameObject book1;
    [SerializeField] private GameObject book2;
    [SerializeField] private GameObject book3;

    [SerializeField] private GameObject btn_close_book1;
    [SerializeField] private GameObject btn_close_book2;
    [SerializeField] private GameObject btn_close_book3;

    [SerializeField] private DialogData dialogData;

    void Start()
    {
        AudioSetup.instance.playMusic();  
        playerController = player.GetComponent<PlayerController>();
        if (SaveLoadManager.Instance.SaveFileExists())
        {
            LoadGame();
        }
        else
        {
            StartNewGame();
        }
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Desa Pejuang")
        {
            if (playerData.priamisterius == false)
            {
                panelDialog.SetActive(true);
                GameObject CP = GameObject.Find("Canvas_Panel");
                CP.GetComponent<DialogManager>().IndexDialog = 0;
                CP.GetComponent<DialogManager>().DialogData = dialogData;
                CP.GetComponent<DialogManager>().SetDialog(0);
            }
            else
            {
                Destroy(GameObject.Find("PriaMisterius"));
            }
        }
        else
        {
            Debug.Log("Do Nothing");
        }
    }
    void StartNewGame()
    {
        // Inisialisasi posisi awal pemain atau data permainan baru lainnya
        PlayerData playerData = new PlayerData();
        SaveLoadManager.Instance.SaveGame(playerData);
        Debug.Log("Ini ngulang / newgame");
    }
    void LoadGame()
    {
        // Load data permainan dari save file
        playerData = SaveLoadManager.Instance.LoadGame();
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Desa Pejuang" && playerData.priamisterius == true)
        {
            Destroy(GameObject.Find("NPC_PriaMisterius"));
        }  
    }
    public void OpenBackpack()
    {
        AudioSetup.instance.playSfx("sfx_button");
        if (canvasTas.activeSelf == false && canvasUI.activeSelf == true)
        {
            canvasTas.SetActive(true);
            canvasUI.SetActive(false);
            playerData = SaveLoadManager.Instance.LoadGame();
            if (playerData.book1 == true)
            {
                btn_book1.SetActive(true);
            }
            else
            {
                btn_book1.SetActive(false); 
            }
            if (playerData.book2 == true)
            {
                btn_book2.SetActive(true);
            }
            else
            {
                btn_book2.SetActive(false);
            }
            if (playerData.book3 == true)
            {
                btn_book3.SetActive(true);
            }
            else
            {
                btn_book3.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Ngebug panelnya");
        }
    }
    public void CloseBackpack()
    {
        AudioSetup.instance.playSfx("sfx_button");
        if (canvasTas.activeSelf == true && canvasUI.activeSelf == false)
        {
            canvasTas.SetActive(false);
            canvasUI.SetActive(true);
        }
    }
    public void OpenandCloseBook1()
    {
        AudioSetup.instance.playSfx("sfx_button");
        if (book1.activeSelf == false)
        {
            canvasTas.SetActive(false);
            book1.SetActive(true);
            btn_close_book1.SetActive(true);
        }
        else
        {
            canvasTas.SetActive(true);
            book1.SetActive(false);
            btn_close_book1.SetActive(false);
        }
    }
    public void OpenandCloseBook2()
    {
        AudioSetup.instance.playSfx("sfx_button");
        if (book2.activeSelf == false)
        {
            canvasTas.SetActive(false);
            book2.SetActive(true);
            btn_close_book2.SetActive(true);
        }
        else
        {
            canvasTas.SetActive(true);
            book2.SetActive(false);
            btn_close_book2.SetActive(false);
        }
    }
    public void OpenandCloseBook3()
    {
        AudioSetup.instance.playSfx("sfx_button");
        if (book3.activeSelf == false)
        {
            canvasTas.SetActive(false);
            book3.SetActive(true);
            btn_close_book3.SetActive(true);
        }
        else
        {
            canvasTas.SetActive(true);
            book3.SetActive(false);
            btn_close_book3.SetActive(false);
        }
    }
    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
