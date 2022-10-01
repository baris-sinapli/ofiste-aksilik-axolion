using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBG : MonoBehaviour
{
    [SerializeField] private GameObject backGround;

    private void EnableBackground()
    {
        backGround.SetActive(true);
    }

    private void DisableBackground()
    {
        backGround.SetActive(false);
    }
}
