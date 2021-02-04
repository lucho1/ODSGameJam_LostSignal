using UnityEngine;

public class SimpleTimer
{

    public float StartTime = 10.0f;
    public float StopTime = 0.0f;
    float m_FinishTime = 0.0f;
    public bool Running = false;


    public void Begin()
    {
        m_FinishTime = Time.time + StartTime;
    }
    public void Stop()
    {
        Running = false;
        StopTime = Time.time;
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
        return (m_FinishTime - Time.time) <= 0;
    }

    public string GetTimeString()
    {
        return string.Format("{0:00}:{1:00}", GetMinutes(), GetSeconds());
    }
}
