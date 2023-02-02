using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI[] showTime;

    float time = 0.0f; // 정산 시간 지났는지 체크하는 변수
    float totalTime = 0.0f; // 게임 시간 알려주는 변수
    public bool isTimerActive = false;

    private int minute;
    private int second;

    // getter, setter
    public float RunningTime
    {
        get { return time; }
        set { time = value; }
    }

    public int Minute
    {
        get { return minute; }
    }
    public int Second
    {
        get { return second; }
    }

    public void Init()
    {
        time = 0.0f;
        totalTime = 0.0f;
        isTimerActive = false;
    }

    public void TimerStart()
    {
        time = 0.0f;
        totalTime = 0.0f;
        isTimerActive = true;
    }

    public void TimeInit()
    {
        time = 0.0f;
        isTimerActive = false;
    }

    public void ProgressTimer()
    {
        isTimerActive = true;
    }

    public void TimerStop()
    {
        isTimerActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerActive) { return; }

        time += Time.deltaTime;
        totalTime += Time.deltaTime;

        // showTime[0]: Minute
        // showTime[1]: Second
        minute = ((int)totalTime / 60 % 60);
        second = ((int)totalTime % 60);

        showTime[0].text = minute / 10 > 0 ? minute.ToString() : "0" + minute.ToString();
        showTime[1].text = second / 10 > 0 ? second.ToString() : "0" + second.ToString();
    }
}
