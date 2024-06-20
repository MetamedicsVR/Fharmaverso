using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDownTimer : MonoBehaviour
{
    public UnityEvent timerStarted;
    public UnityEvent timerStopped;
    public UnityEvent timerEnded;

    public TextMeshProUGUI remainingTMP;

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
            remainingTMP.text = remainingTime.ToString("F0");
        }
    }

    public void StartTimer()
    {
        if (!counting)
        {
            remainingTime = totalTime;
            counting = true;
            timerStarted.Invoke();
        }
    }

    public void StopTimer()
    {
        if (counting || remainingTime != totalTime)
        {
            counting = false;
            timerStopped.Invoke();
            remainingTime = totalTime;
            remainingTMP.text = remainingTime.ToString("F0");
        }
    }

    public bool IsCounting()
    {
        return counting;
    }
}
