using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime; 
    public float CurrentTime { get { return currentTime; } set { currentTime = value; } }

    private bool running;

    private float debugTimer;

    private void Update()
    {
        if (!running)
        {
            return;
        }

        CurrentTime -= Time.deltaTime;
        debugTimer -= Time.deltaTime;

        if (debugTimer <= 0)
        {
            Debug.Log("Time Remaining: " + CurrentTime.ToString("F2"));
            debugTimer = 1f;
        }

        // Debug.Log("Time Remaining: " + currentTime.ToString("F2"));
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