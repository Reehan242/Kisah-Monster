using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject PanelDialog;
    [SerializeField] private GameObject PanelBattle;
    [SerializeField] private GameObject DPAD;
    [SerializeField] private GameObject bobj;
    public DialogData DialogData;
    [SerializeField] private TextMeshProUGUI dialogBox;
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private Button btn_next;
    [SerializeField] private GameObject UItas;
    public int IndexDialog;
    void Start()
    {
        UItas = GameObject.Find("btn_tas");
        DPAD = GameObject.Find("DPAD");
        bobj = GameObject.Find("Button_Objektif");
        IndexDialog = 0;
    }
    public void SetDialog(int dialogIndex)
    {
        Debug.Log("Nama dialog nya adalah" + DialogData.name);
        
        UItas.SetActive(false);
        DPAD.SetActive(false);
        if(bobj != null) { bobj.SetActive(false); }
        dialogBox.text = DialogData.dialogs[dialogIndex].dialogText;
        Name.text = DialogData.dialogs[dialogIndex].talkerName;
        if (DialogData.dialogs[dialogIndex].avatar == null )
        {
            Color c = avatar.GetComponent<Image>().color;
            c.a = 0;
            avatar.GetComponent<Image>().color = c;
        }
        else
        {
            avatar.sprite = DialogData.dialogs[dialogIndex].avatar;
            Color c = avatar.GetComponent<Image>().color;
            c.a = 1;
            avatar.GetComponent<Image>().color = c;
        }
    }
    public void CheckNextDialog()
    {
        AudioSetup.instance.playSfx("sfx_dialog");
        if (IndexDialog < DialogData.dialogs.Length)
        {
            if (DialogData.dialogs[IndexDialog].cut == false 
                && DialogData.dialogs[IndexDialog].cut2 == false 
                && DialogData.dialogs[IndexDialog].end == false)
            {
                IndexDialog++;
                SetDialog(IndexDialog);
                Debug.Log("cut false ketrigger"+ IndexDialog);
            }
            else if (DialogData.dialogs[IndexDialog].cut == true)
            {
                
                Debug.Log("cut true ketrigger" + IndexDialog);
                IndexDialog++;
                if (DialogData.name == "PriaMisterius")
                {
                    SetDialog(IndexDialog);
                    Destroy(GameObject.Find("NPC_PriaMisterius"));

                }else if(DialogData.name == "SecretLever")
                {
                    GameObject.Find("SecretLever").GetComponent<interaksi>().mechanismAnimation.Play("ObjectUp");
                    SetDialog(IndexDialog);
                }
                else
                {
                    SetDialog(IndexDialog);
                    PanelDialog.SetActive(false);
                    UItas.SetActive(true);
                    DPAD.SetActive(true);
                    if (bobj != null) { bobj.SetActive(true); }
                }
            }
            else if (DialogData.dialogs[IndexDialog].cut2 == true)
            {
                IndexDialog++;

                SetDialog(IndexDialog);
                PanelDialog.SetActive(false);
                PanelBattle.SetActive(true);
            }
            else if (DialogData.dialogs[IndexDialog].end == true)
            {
                if (DialogData.name == "PriaMisterius")
                {
                    PlayerData playerData = SaveLoadManager.Instance.LoadGame();
                    playerData.priamisterius = true;
                    SaveLoadManager.Instance.SaveGame(playerData);

                }else if(DialogData.name == "PenjagaKedai")
                {
                    PlayerData playerData = SaveLoadManager.Instance.LoadGame();
                    playerData.penjagakedai = true;
                    SaveLoadManager.Instance.SaveGame(playerData);
                }else if(DialogData.name == "DoorToPhobos")
                {
                    if(IndexDialog >= 4)
                    {
                        GameObject.Find("DoorToPhobos").GetComponent<interaksi>().mechanismAnimation.Play("DoorUp");
                    }
                    
                }else if(DialogData.name == "SecretLever")
                {
                    Destroy(GameObject.Find("SecretLever"));
                }else if(DialogData.name == "Phobos_2")
                {
                    Destroy(GameObject.Find("BlockingPortal"));
                    GameObject.Find("Phobos").GetComponent<interaksi>().mechanismAnimation.Play("PortalUp");
                    Destroy(GameObject.Find("Phobos").GetComponent<BoxCollider>());
                }
                else if (DialogData.name == "Phobos_1")
                {
                    SceneManager.LoadScene("Cutscene Phobos");
                }
                PanelDialog.SetActive(false);
                UItas.SetActive(true);
                DPAD.SetActive(true);
                if (bobj != null) { bobj.SetActive(true); }
            }
        }
        
    }
}
