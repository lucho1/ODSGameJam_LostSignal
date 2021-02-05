using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : Singleton<SoundsManager>
{
    [SerializeField]
    private AudioClip m_NewPlanetSound, m_FailedConstructionSound, m_ConstructionSound;

    public static AudioClip NewPlanetSound          { get { return Instance.m_NewPlanetSound; }             set { Instance.m_NewPlanetSound = value; } }
    public static AudioClip FailedConstructionSound { get { return Instance.m_FailedConstructionSound; }    set { Instance.m_FailedConstructionSound = value; } }
    public static AudioClip ConstructionSound       { get { return Instance.m_ConstructionSound; }          set { Instance.m_ConstructionSound = value; } }


    private static AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip sound)
    {
        m_AudioSource.Stop();
        m_AudioSource.clip = sound;
        m_AudioSource.Play();
    }
}
