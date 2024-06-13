using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountDownTimer : MonoBehaviour
{
    public UnityEvent timerStarted;
    public UnityEvent timerReset;
    public UnityEvent timerStopped;
    public UnityEvent timerPaused;
    public UnityEvent timerEnded;

    public float totalTime;
    private float remainingTime;
    private bool counting;

    private void Update()
    {
        if(counting)
        {
            remainingTime -= Time.deltaTime;
            if(remainingTime < 0)
            {
                remainingTime = 0;
                counting = false;
                timerEnded.Invoke();
            }
        }
    }

    public void StartTimer()
    {
        if (!counting)
        {
            counting = true;
            timerStarted.Invoke();
        }
    }

    public void ResetTimer()
    {
        if (remainingTime != totalTime)
        {
            remainingTime = totalTime;
            timerReset.Invoke();
        }
    }

    public void StopTimer()
    {
        if (counting || remainingTime != totalTime)
        {
            remainingTime = totalTime;
            counting = false;
            timerStopped.Invoke();
        }
    }

    public void PauseTimer()
    {
        if (counting)
        {
            counting = false;
            timerPaused.Invoke();
        }
    }
}
