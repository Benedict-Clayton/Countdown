using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Singleton Stuff
    static private UIManager instance;
    static public UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no UIManager instance in the scene.");
            }
            return instance;
        }
    }

    [Header("UI References")]
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private TMP_Text countdownText;
    public TMP_Text CountdownText => countdownText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text instructionText;

    private void Awake()
    {
        instance = this;
    }

    public void SetTarget(float targetTime)
    {
        targetText.text = targetTime.ToString("F2");
    }


    public void SetCountdown(float currentTime)
    {
        countdownText.text = currentTime.ToString("F2");
    }
    public void SetResult(string result)
    {
        resultText.text = result;
    }


    public void SetScore(int score)
    {
        scoreText.text = "SCORE: " + score;
    }


    public void SetInstruction(string instruction)
    {
        instructionText.text = instruction;
    }

    public void ClearResult()
    {
        resultText.text = "";
    }

    public void HideTimer()
    {
        countdownText.gameObject.SetActive(false);
    }
}
