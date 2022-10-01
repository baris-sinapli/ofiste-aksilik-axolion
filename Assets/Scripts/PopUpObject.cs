using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pop-up Object", menuName = "ScriptableObjects/PopUpObject")]
public class PopUpObject : ScriptableObject
{
    [Header("Pop-up Message")]
    public int level;
    [TextArea] public string title;
    [TextArea] public string description;

    [Space]

    [Header("Option 1")]
    [TextArea] public string optionText1;
    [TextArea] public string resultTitle1;
    [TextArea] public string resultDescription1;
    public int workLoad1;
    public int money1;
    
    [Space]

    [Header("Option 2")]
    [TextArea] public string optionText2;
    [TextArea] public string resultTitle2;
    [TextArea] public string resultDescription2;
    public int workLoad2;
    public int money2;
}
