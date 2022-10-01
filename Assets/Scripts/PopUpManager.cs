using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [Header("Required Game Objects")]
    [SerializeField] private Animator warningAnimator;
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GameObject popUpMessage;
    [SerializeField] private GameObject popUpResultMessage;
    [SerializeField] private Slider workloadSlider;
    [SerializeField] private Slider moneySlider;

    [Header("Pop-up Timer")]
    
    [SerializeField] private float waitingAmount = 20f;
    [SerializeField] private float remainingTime;
    private bool isTimerActive = true;

    [Header("Pop-up Objects")]
    [SerializeField] private PopUpObject[] lvl1Objects;
    [SerializeField] private PopUpObject[] lvl2Objects;
    [SerializeField] private PopUpObject[] lvl3Objects;
    [SerializeField] private PopUpObject[] lvl4Objects;
    [SerializeField] private PopUpObject[] lvl5Objects;

    private PopUpObject selectedPopUp;
    private float changeOfWorkloadSlider;
    private float changeOfMoneySlider;
    private int level;

    private void Awake()
    {
        remainingTime = waitingAmount;
    }

    private void Start()
    {
        DetermineLevel();
    }

    private void DetermineLevel()
    {
        if (workloadSlider.value >= 90) level = 1;
        else if (workloadSlider.value >= 70) level = 2;
        else if (workloadSlider.value >= 50) level = 3;
        else if (workloadSlider.value >= 30) level = 4;
        else level = 5;
    }

    void Update()
    {
        // Step 1: Give alert to screen after {{waitingAmount}} seconds.
        if (waitingAmount > 0f && isTimerActive)
        {
            remainingTime -= Time.deltaTime;
        }
        if (remainingTime <= 0f && isTimerActive && !warningAnimator.GetBool("isWarning"))
        {
            warningAnimator.SetBool("isWarning", true);
            isTimerActive = false;
        }

        // Step 2: If timer ready, show Pop-up after click screen
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "warning")
                {
                    ShowPopUp();
                    PrintPopUp(); // Step 3: Print random pop-up object on screen
                    remainingTime = waitingAmount;
                }
            }
        }

        

        // Step 6: Reset the timer to give alert

    }

    private void PrintPopUp()
    {
        DetermineLevel();
        int random;
        
        switch (level)
        {
            case 1:
                random = RandomWithExclusion(0, lvl1Objects.Length);
                selectedPopUp = lvl1Objects[random];
                break;
            case 2:
                random = RandomWithExclusion(0, lvl2Objects.Length);
                selectedPopUp = lvl2Objects[random];
                break;
            case 3:
                random = RandomWithExclusion(0, lvl3Objects.Length);
                selectedPopUp = lvl3Objects[random];
                break;
            case 4:
                random = RandomWithExclusion(0, lvl4Objects.Length);
                selectedPopUp = lvl4Objects[random];
                break;
            case 5:
                random = RandomWithExclusion(0, lvl5Objects.Length);
                selectedPopUp = lvl5Objects[random];
                break;
            default:
                selectedPopUp = null;
                break;
        }
        if (selectedPopUp != null)
        {
            popUpMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedPopUp.title;
            popUpMessage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedPopUp.description;
            popUpMessage.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedPopUp.optionText1;
            popUpMessage.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedPopUp.optionText2;
        }
    }

    public void ShowPopUp()
    {
        warningAnimator.SetBool("isWarning", false);
        popUpPanel.SetActive(true);
        popUpMessage.SetActive(true);
    }

    int excludeLastRandNum;
    bool firstRun = true;

    int RandomWithExclusion(int min, int max)
    {
        int result;
        if (firstRun)
        {
            result = Random.Range(min, max);
            excludeLastRandNum = result;
            firstRun = false;
            return result;
        }
        result = Random.Range(min, max - 1);
        result = (result < excludeLastRandNum) ? result : result + 1;
        excludeLastRandNum = result;
        return result;
    }

    // Step 4: After selecting an option, change "pop-up screen" to "results screen"
    public void OptionOneSelected()
    {
        popUpMessage.SetActive(false);
        popUpResultMessage.SetActive(true);

        popUpResultMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedPopUp.resultTitle1;
        popUpResultMessage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedPopUp.resultDescription1;

        changeOfMoneySlider = selectedPopUp.money1;
        changeOfWorkloadSlider = selectedPopUp.workLoad1;
    }

    // Step 4: After selecting an option, change "pop-up screen" to "results screen"
    public void OptionTwoSelected()
    {
        popUpMessage.SetActive(false);
        popUpResultMessage.SetActive(true);

        popUpResultMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedPopUp.resultTitle2;
        popUpResultMessage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedPopUp.resultDescription2;

        changeOfMoneySlider = selectedPopUp.money2;
        changeOfWorkloadSlider = selectedPopUp.workLoad2;
    }

    // Step 5: After closing the results screen, update progress bars
    public void CloseResultPopup()
    {
        popUpPanel.SetActive(false);
        popUpResultMessage.SetActive(false);

        StartCoroutine(LerpSlider(workloadSlider, changeOfWorkloadSlider, 2f));
        StartCoroutine(LerpSlider(moneySlider, changeOfMoneySlider, 2f));

        isTimerActive = true;
    }

    private IEnumerator LerpSlider(Slider slider, float amount, float seconds)
    {
        float currentValue = slider.value;
        float animationTime = 0f;

        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            slider.value = Mathf.Lerp(currentValue, currentValue + amount, lerpValue);
            yield return null;
        }
    }

}
