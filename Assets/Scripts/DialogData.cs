using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogData", menuName = "DialogData")]
public class DialogData : ScriptableObject
{
    [System.Serializable]
    public struct Dialog
    {
        public string talkerName;
        public string dialogText;
        public Sprite avatar;
        public bool cut;
        public bool cut2;
        public bool end;
    }
    public Dialog[] dialogs;
}
