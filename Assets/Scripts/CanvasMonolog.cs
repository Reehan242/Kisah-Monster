using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMonolog : MonoBehaviour
{
    public int IndexMax;
    public int IndexCurrent;
    [SerializeField] private Button a;
    [SerializeField] private Button b;
    private void OnEnable()
    {
        GetComponent<Animator>().Play("FadeIn");
    }
    private void OnDisable()
    {
        GetComponent<Animator>().Play("FadeOut");
    }
    void Awake()
    {   
        PlayerData playerData = SaveLoadManager.Instance.LoadGame();
        IndexMax = countIndexMax(playerData);
        IndexCurrent = 0; 
        displayPanel();
    }

    public void changeIndexCurrent(int indexValue)
    {
        IndexCurrent = indexValue;   
    }

    public int countIndexMax(PlayerData playerData)
    {
        if (playerData.penjagakedai == true && playerData.battleCleared == 0)
        {
            return 2;
        }
        else if (playerData.penjagakedai == true && playerData.battleCleared == 1)
        {
            return 3;
        }
        else if (playerData.penjagakedai == true && playerData.battleCleared == 2) 
        {
            return 4;
        }
        else if (playerData.penjagakedai == true && playerData.battleCleared == 3)
        {
            return 5;
        }
        else
        {
            return 1;
        }
    }

    public void displayPanel()
    {
        //ini buat load page yang sesuai index current sisanya jadi scale 0
        for(int i = 0; i < 7; i++)
        {
            if(GameObject.Find("Page"+i) != null)
            {
                Debug.Log("Page" + i);
                if (i == IndexCurrent)
                {
                    GameObject.Find("Page" + i).transform.localScale = Vector2.one;
                }
                else
                {
                    GameObject.Find("Page" + i).transform.localScale = Vector2.zero;
                }
            }
            
            
        }
        //ini ngitung index buat nongolin button next/prev nya
        if (IndexCurrent == IndexMax)
        {
            a.gameObject.SetActive(false);
            b.gameObject.SetActive(true);
        }
        else if (IndexCurrent == 0)
        {
            a.gameObject.SetActive(true);
            b.gameObject.SetActive(false);
        }
        else
        {
            a.gameObject.SetActive(true);
            b.gameObject.SetActive(true);
        }
    }
    //ini buat pindah halaman di canvas monolog
    public void nextPage()
    {
        AudioSetup.instance.playSfx("sfx_button");
        if (IndexCurrent < IndexMax)
        {
            IndexCurrent += 1;
            displayPanel();
        }
    }
    public void previousPage()
    {
        AudioSetup.instance.playSfx("sfx_button");
        if (IndexCurrent > 0)
        {
            IndexCurrent -= 1;
            displayPanel();
        }
    } 
}
