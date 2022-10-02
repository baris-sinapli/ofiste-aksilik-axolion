using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject bottomTalkPanel;
    [SerializeField] private TextMeshProUGUI statement;
    [SerializeField] private Image personImage;
    [SerializeField] private Animator canvasAnimator;

    [Header("Sources")]
    [SerializeField] private Sprite[] sourceImages;
    [SerializeField] private DialogObject[] dialogs;

    private int dialogIndex = 0;
    private bool isDialogEnded = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "pass" && !isDialogEnded)
                {
                    if (!bottomTalkPanel.activeInHierarchy)
                    {
                        bottomTalkPanel.SetActive(true);
                    }
                    NextDialog();
                }
            }
            
        }
    }

    private void NextDialog()
    {
        if(dialogIndex >= dialogs.Length)
        {
            canvasAnimator.SetTrigger("FadeCanvas");
            isDialogEnded = true;
            return;
        }

        int spriteNumber = (int)dialogs[dialogIndex].sprite;
        personImage.sprite = sourceImages[spriteNumber];
        statement.text = dialogs[dialogIndex].text;
        dialogIndex++;
    }
}
