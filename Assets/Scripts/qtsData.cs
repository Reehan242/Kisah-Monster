using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New QuestionData", menuName ="QuestionData")]
public class qtsData : ScriptableObject
{
    public int enemyIndex;
    [System.Serializable]
    public struct Question
    {
        public string questionText;
        public string[] replies;
        public int correctReplayIndex;
    }
    public Question[] questions;
}
