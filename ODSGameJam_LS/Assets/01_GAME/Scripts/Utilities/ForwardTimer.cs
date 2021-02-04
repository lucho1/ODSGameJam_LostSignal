using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardTimer : MonoBehaviour
{
    public float StartingTime = 0.0f;
    public bool BeginStarted = true;

    private float m_Time = 0.0f;
    private bool m_TimerActive = false;

    // --- Getters ---
    public float GetTime()  { return m_Time; }
    public bool IsRunning() { return m_TimerActive; }

    // Start is called before the first frame update
    public void Start()
    {
        m_Time = StartingTime;
        if (BeginStarted)
            Play();
    }

    // Update is called once per frame
    public void Update()
    {
        if (m_TimerActive)
            m_Time += Time.deltaTime;
    }    

    public void Play()
    {
        m_TimerActive = true;
    }

    public void Pause()
    {
        m_TimerActive = false;
    }

    public void RestartAndStop()
    {
        m_Time = StartingTime;
        m_TimerActive = false;
    }

    public void RestartAndPlay()
    {
        m_Time = StartingTime;
        m_TimerActive = true;
    }

    public void RestartFromZeroAndStop()
    {
        m_Time = 0.0f;
        m_TimerActive = false;
    }

    public void RestartFromZeroAndPlay()
    {
        m_Time = 0.0f;
        m_TimerActive = true;
    }
}
