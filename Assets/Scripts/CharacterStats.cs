using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterStats", menuName = "CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public int health;
    public int damage;
}
