using UnityEngine;

public class SimpleTimer
{

    public float Duration = 10.0f;
    public float StopTime = 0.0f;
    float m_FinishTime = 0.0f;
    public bool Running = false;


    public void Begin()
    {
        m_FinishTime = Time.time + Duration;
        Running = true;
    }
    public void Stop()
    {
        if (!Running)
            return;

        Running = false;
        StopTime = Time.time;
    }

    public void Resume() 
    {
        if (Running)
            return;

        m_FinishTime = Time.time + (m_FinishTime - StopTime);
        Running = true;
    }

    public int GetMinutes()
    {
        int secondsLeft = GetTimeLeftInSeconds();
        if (secondsLeft == 0)
            return 0;
        else
            return Mathf.FloorToInt(secondsLeft / 60.0f);
    }

    public int GetSeconds()
    {
        int secondsLeft = GetTimeLeftInSeconds();
        if (secondsLeft == 0)
            return 0;
        else
            return Mathf.FloorToInt(secondsLeft % 60.0f);
    }

    public int GetTimeLeftInSeconds()
    {
        float TimeLeft = 0;
        if (Running)
            TimeLeft = Mathf.Max(0, m_FinishTime - Time.time);
        else
            TimeLeft = Mathf.Max(0, m_FinishTime - StopTime);

        return Mathf.FloorToInt(TimeLeft);
    }

    public bool Finished() {
        float TimeLeft = 0;
        if (Running)
            TimeLeft = m_FinishTime - Time.time;
        else
            TimeLeft = m_FinishTime - StopTime;

        return TimeLeft <= 0;
    }

    public string GetTimeString()
    {
        return string.Format("{0:00}:{1:00}", GetMinutes(), GetSeconds());
    }
}
