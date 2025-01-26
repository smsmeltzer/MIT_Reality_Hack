using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public bool timerRunning = false;
    public float timerCount = 120.0f;
    public bool TimerDone = false;

    private EventManager EventManager;

    private void Start()
    {
        EventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
    }
    private void Update()
    {
        if (timerRunning)
        {
            timerCount -= Time.deltaTime;
            timerText.text = "Count Down till Landing: \n" + timerCount.ToString();
            if (timerCount <= 0.0f)
            {
                timerText.text = "Landed on the Moon!";

                TimerDone = true;
                timerRunning = false;
                EventManager.EndGame();
            }

        }
    }
    public void StartTimer()
    {
        timerRunning = true;
    }

    public bool TimerEnded()
    {
        return TimerDone;
    }
}
