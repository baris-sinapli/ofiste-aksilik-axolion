using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog Object", menuName = "ScriptableObjects/DialogObject")]
public class DialogObject : ScriptableObject
{
    public CharacterSprite sprite;
    [TextArea] public string text;
}

public enum CharacterSprite
{
    NormanShout,
    NormanSad,
    NormanHappy,
    NormanAngry,
    HumbledoorNormal,
    HumbledoorHappy
}
