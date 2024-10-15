using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaksi : MonoBehaviour
{
    private int indexDialog;
    public DialogData dialogData;
    public Animator mechanismAnimation;
    private bool doorOpened;
    private void Start()
    {
        doorOpened = false;
        PlayerData playerData = SaveLoadManager.Instance.LoadGame();
        switch (gameObject.name)
        {
            case "book1":
                if(playerData.book1 == true)
                {
                    Destroy(gameObject);
                }break;
            case "book2":
                if(playerData.book2 == true)
                {
                    Destroy(gameObject);
                }break;
            case "book3":
                if(playerData.book3 == true)
                {
                    Destroy(gameObject);
                }break;
            case "magickey":
                if(playerData.key == true)
                {
                    Destroy(gameObject);           
                }
                break;
            case "SecretLever":
                if (playerData.key == true)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
    //ini buat cek saat interaksi 
    public void run_Interact()
    {
        PlayerData playerData = SaveLoadManager.Instance.LoadGame();
        if (gameObject.tag == "npc")
        {
            if (gameObject.name == "Slime")
            {
                if (playerData.battleCleared >= 1)
                {
                    
                    indexDialog = 1;
                }
                else
                {
                    indexDialog = 0;
                }
            }
            else if (gameObject.name == "Minotaur")
            {
                if (playerData.battleCleared >= 2)
                {
                    indexDialog = 1;
                }
                else
                {
                    indexDialog = 0;
                }
            }
            else if (gameObject.name == "Golem")
            {
                if (playerData.battleCleared >= 3)
                {
                    indexDialog = 9;
                }
                else
                {
                    indexDialog = 0;
                }
            }
            else
            {
                indexDialog = 0;
            }
            GameObject CP = GameObject.Find("Canvas_Panel");
            CP.GetComponent<DialogManager>().IndexDialog = indexDialog;
            CP.GetComponent<DialogManager>().DialogData = dialogData;
            CP.GetComponent<DialogManager>().SetDialog(indexDialog);
        }
        else if(gameObject.tag == "item")
        {
            if (gameObject.name == "book1")
            {
                playerData.book1 = true;
                
            }
            else if (gameObject.name == "book2")
            {
                playerData.book2 = true;
                
            }
            else if (gameObject.name == "book3")
            {
                playerData.book3 = true;
                
            }
            else if (gameObject.name == "magickey")
            {
                playerData.key = true;
            }
            
            SaveLoadManager.Instance.SaveGame(playerData);
            GameObject CP = GameObject.Find("Canvas_Panel");
            CP.GetComponent<DialogManager>().IndexDialog = indexDialog;
            CP.GetComponent<DialogManager>().DialogData = dialogData;
            CP.GetComponent<DialogManager>().SetDialog(indexDialog);
            Destroy(gameObject);
        }
        else if(gameObject.tag == "door")
        {

            if (gameObject.name == "DoorToPhobos")
            {
                if(playerData.key == true)
                {
                    if(doorOpened == false)
                    {
                        indexDialog = 4;
                        doorOpened = true;
                    }
                    else
                    {
                        indexDialog = 5; 
                    } 
                }
                else
                {
                    indexDialog = 0;
                }
            }
            else if (gameObject.name == "SecretLever")
            {
                indexDialog = 0;
            }
            GameObject CP = GameObject.Find("Canvas_Panel");
            CP.GetComponent<DialogManager>().IndexDialog = indexDialog;
            CP.GetComponent<DialogManager>().DialogData = dialogData;
            CP.GetComponent<DialogManager>().SetDialog(indexDialog);
        } 
    }
}
