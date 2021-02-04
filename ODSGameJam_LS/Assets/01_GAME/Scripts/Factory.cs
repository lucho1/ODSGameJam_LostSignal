using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{

    public bool EcoFactory = false;

    private float m_contaminationAmount = 0.0f;
    private float m_nextResourceTime = 0.0f;
    private float m_nextContaminationTime = 0.0f;

    private PlanetScript myPlanet;
    // Start is called before the first frame update
    void Start()
    {
        myPlanet = gameObject.GetComponent<PlanetScript>();
        m_nextResourceTime = Time.time + GameOptions.ResourceRefreshRate;
        m_nextContaminationTime = Time.time + GameOptions.PollutionRefreshRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= m_nextContaminationTime) {
            m_nextContaminationTime = Time.time + GameOptions.PollutionRefreshRate;
            myPlanet.currentHealth -= GameOptions.StandardFactoryContamination;
        }

        if (Time.time >= m_nextResourceTime) {
            m_nextResourceTime = Time.time + GameOptions.PollutionRefreshRate;
            // Add resources here
        }
    }
}
