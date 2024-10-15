using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasObjektif : MonoBehaviour
{
    [SerializeField] private GameObject PanelObj;
    [SerializeField] private TextMeshProUGUI TextObj;
    private bool previousState;
    void Start()
    {
        if (PanelObj != null)
        {
            previousState = PanelObj.activeSelf;
        }
    }
    void Update()
    {
        if (PanelObj != null)
        {
            bool currentState = PanelObj.activeSelf;
            if (currentState != previousState)
            {
                if (currentState)
                {
                    OnPanelEnabled();
                }
                else
                {
                    OnPanelDisabled();
                }
                previousState = currentState;
            }
        }
    }
    private void OnPanelEnabled()
    {
        PlayerData playerData = SaveLoadManager.Instance.LoadGame();
        if(playerData != null )
        {
            if (playerData.priamisterius == true && playerData.penjagakedai == false)
            {
                TextObj.text = "Bertanya pada penduduk desa mengenai kalung yang kau dapatkan dan maksud dari perkataan pria misterius tadi";
            }
            else if (playerData.penjagakedai == true && playerData.battleCleared < 1)
            {
                TextObj.text = "Pergi ke Rawa Luminos dan bicara pada Monster yang menghalangi jalan mu ";
            }
            else if (playerData.penjagakedai == true && playerData.battleCleared < 2)
            {
                TextObj.text = "Pergi ke Gurun Esteril dan bicara pada Monster yang menghalangi jalan mu";
            }
            else if (playerData.penjagakedai == true && playerData.battleCleared < 3)
            {
                TextObj.text = "Pergi ke Reruntuhan Arkhamar dan bicara pada Monster yang menghalangi jalan mu";
            }
            else if (playerData.penjagakedai == true && playerData.battleCleared >= 3)
            {
                TextObj.text = "Pergi ke Jurang Kehampaan, dan temui sosok yang mengetahui kebenarannya";
            }
            else
            {
                TextObj.text = "";
            }
        }
    }
    private void OnPanelDisabled()
    {
        TextObj.text = "";
    }
    public void Button_click()
    {
        AudioSetup.instance.playSfx("sfx_button");

    }
}
