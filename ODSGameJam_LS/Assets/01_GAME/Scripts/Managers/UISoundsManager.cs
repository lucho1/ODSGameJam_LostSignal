using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundsManager : Singleton<UISoundsManager>
{
    [SerializeField]
    private AudioClip m_PaySound, m_SwitchPlanetSound, m_DebtIncreasesSound;

    public static AudioClip PaySound            { get { return Instance.m_PaySound; }           set { Instance.m_PaySound = value; } }
    public static AudioClip SwitchPlanetSound   { get { return Instance.m_SwitchPlanetSound; }  set { Instance.m_SwitchPlanetSound = value; } }
    public static AudioClip DebtIncreasesSound  { get { return Instance.m_DebtIncreasesSound; } set { Instance.m_DebtIncreasesSound = value; } }


    private static AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip sound)
    {
        if (sound == SwitchPlanetSound)
            m_AudioSource.volume = 0.05f;
        else
            m_AudioSource.volume = 0.5f;

        m_AudioSource.Stop();
        m_AudioSource.clip = sound;
        m_AudioSource.Play();
    }
}
