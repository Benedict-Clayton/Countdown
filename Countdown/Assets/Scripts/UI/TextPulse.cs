using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPulse : MonoBehaviour
{
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float pulseAmount = 0.03f;
    private bool isActive;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;

        GameManager.OnStateChanged += OnStateChange;
    }

    private void OnDestroy()
    {
        GameManager.OnStateChanged -= OnStateChange;
    }

    private void Update()
    {
        if (!isActive)
        {
            transform.localScale = originalScale;
            return;
        }

        float pulse = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        transform.localScale = originalScale * pulse;

    }

    private void OnStateChange(GameManager.State state)
    {
        if (state == GameManager.State.Waiting)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
    }
}