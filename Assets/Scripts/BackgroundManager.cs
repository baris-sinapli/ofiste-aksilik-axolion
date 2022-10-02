using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Sprite[] bgSprites;
    private SpriteRenderer bgSpriteRenderer;
    private int currentStage;
    void Awake()
    {
        bgSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        bgSpriteRenderer.sprite = bgSprites[0];
        currentStage = 0;
    }

    public void NextStage()
    {
        if(currentStage == 3)
        {
            bgSpriteRenderer.sprite = bgSprites[0];
            currentStage = 0;
            return;
        }
        currentStage++;
        bgSpriteRenderer.sprite = bgSprites[currentStage];
    }
}
