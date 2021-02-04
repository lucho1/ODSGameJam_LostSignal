using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{

    public bool EcoFactory = false;

    private float m_nextResourceTime = 0.0f;
    private float m_nextContaminationTime = 0.0f;

    private PlanetScript myPlanet;
    // Start is called before the first frame update
    void Start()
    {
        myPlanet = gameObject.GetComponentInParent<PlanetScript>();
        m_nextResourceTime = Time.time + GameOptions.ResourceRefreshRate;
        m_nextContaminationTime = Time.time + GameOptions.PollutionRefreshRate;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= m_nextContaminationTime) {
            Debug.Log("Adding contamination");
            m_nextContaminationTime = Time.time + GameOptions.PollutionRefreshRate;
            if (EcoFactory)
                m_nextContaminationTime = Time.time + GameOptions.EcoFactoryContamination;
            else
                m_nextContaminationTime = Time.time + GameOptions.StandardFactoryContamination;
            myPlanet.currentHealth -= GameOptions.StandardFactoryContamination;
        }

        if (Time.time >= m_nextResourceTime) {
            Debug.Log("Adding score");
            m_nextResourceTime = Time.time + GameOptions.ResourceRefreshRate;
            if (EcoFactory)
                ResourcesManager.AddResources(GameOptions.EcoFactoryProduction, myPlanet.PlanetId);
            else
                ResourcesManager.AddResources(GameOptions.StandardFactoryProduction, myPlanet.PlanetId);
        }
    }
}
