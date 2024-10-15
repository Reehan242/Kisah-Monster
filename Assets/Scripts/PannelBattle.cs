using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelBattle : MonoBehaviour
{
    private void OnEnable()
    {
        AudioSetup.instance.playSpecificMusic("battlesong");
    }
    private void OnDisable()
    {
        AudioSetup.instance.playMusic();
    }
}
