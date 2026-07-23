using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime; 
    public float CurrentTime { get { return currentTime; } set { currentTime = value; } }

    private bool running;

    void Update()
    {
        if (!running)
        {
            return;
        }

        CurrentTime -= Time.deltaTime;
    }


    public void StartTimer(float countdownTime) 
    { 
        CurrentTime = countdownTime; 
        running = true; 
    }


    public float StopTimer()
    {
        running = false;

        return CurrentTime;
    }
}