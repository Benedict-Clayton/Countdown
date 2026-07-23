using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Singleton Stuff
    static private InputManager instance;
    static public InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no InputManager instance in the scene.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandlePress();
        }
    }
    void HandlePress()
    {
        switch (GameManager.Instance.CurrentState)
        {
            case GameManager.State.Waiting:
                GameManager.Instance.BeginCountdown();
                break;


            case GameManager.State.Countdown:
                GameManager.Instance.StopCountdown();
                break;


            case GameManager.State.Results:
                GameManager.Instance.StartRound();
                break;
        }
    }
}
