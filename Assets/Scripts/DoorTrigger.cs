using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public DialogData dialogData;
    public GameObject canvasNarasi;
    public void door_interact()
    {
        PlayerData playerData = SaveLoadManager.Instance.LoadGame();
        //buat cek door yang diakses itu kemana
        if (gameObject.name == "GateToLuminos")
        {
            if (playerData.penjagakedai == false)
            {
                GameObject CP = GameObject.Find("Canvas_Panel");
                CP.GetComponent<DialogManager>().IndexDialog = 0;
                CP.GetComponent<DialogManager>().DialogData = dialogData;
                CP.GetComponent<DialogManager>().SetDialog(0);
            }
            else
            {   
                canvasNarasi.SetActive(true);
                canvasNarasi.GetComponent<CanvasMonolog>().changeIndexCurrent(2);   
            }
        }else if(gameObject.name == "GateToDesaPejuang")
        {
            canvasNarasi.SetActive(true);
            canvasNarasi.GetComponent<CanvasMonolog>().changeIndexCurrent(1);
            
        }
        else if (gameObject.name == "GateToRawaLuminos")
        {
            if (playerData.penjagakedai == true)
            {
                canvasNarasi.SetActive(true);
                canvasNarasi.GetComponent<CanvasMonolog>().changeIndexCurrent(2);
            }
            else
            {
                //doNothing
            }
        }

        else if(gameObject.name == "GateToGurunEsteril")
        {
            if(playerData.battleCleared >= 1)
            {
                canvasNarasi.SetActive(true);
                canvasNarasi.GetComponent<CanvasMonolog>().changeIndexCurrent(3);   
            }
            else
            {
                //doNothing
            }
        }
        else if (gameObject.name == "GateToArkhamar")
        {
            if (playerData.battleCleared >= 2)
            {
                canvasNarasi.SetActive(true);
                canvasNarasi.GetComponent<CanvasMonolog>().changeIndexCurrent(4);
            }
            else
            {
                //doNothing
            }
        }
        else if (gameObject.name == "GateToJurang")
        {
            if (playerData.battleCleared >= 3)
            {
                canvasNarasi.SetActive(true);
                canvasNarasi.GetComponent<CanvasMonolog>().changeIndexCurrent(5);
            }
            else
            {
                //doNothing
            }
        }
        else if (gameObject.name == "GateToCredit")
        {
            SceneManager.LoadScene("Credit");
        }
        canvasNarasi.GetComponent<CanvasMonolog>().IndexMax = canvasNarasi.GetComponent<CanvasMonolog>().countIndexMax(playerData);
        canvasNarasi.GetComponent<CanvasMonolog>().displayPanel();

    }
}
