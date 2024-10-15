using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float x;
    public float y;
    public float z;
    public int battleCleared;
    public bool book1;
    public bool book2;
    public bool book3;
    public bool priamisterius;
    public bool penjagakedai;
    public bool key;
    public PlayerData()
    {  
        battleCleared = 0;
        book1 = false;
        book2 = false;
        book3 = false;
        priamisterius = false;
        penjagakedai = false;
        key = false;

    }  
}

