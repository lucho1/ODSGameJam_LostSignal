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
        if (!myPlanet || myPlanet.currentHealth == 0) {
            this.enabled = false;
            return;
        }


        if (Time.time >= m_nextContaminationTime)
        {
            m_nextContaminationTime = Time.time + GameOptions.PollutionRefreshRate;
            if (EcoFactory)
                m_nextContaminationTime = Time.time + GameOptions.EcoFactoryContamination;
            else
                m_nextContaminationTime = Time.time + GameOptions.StandardFactoryContamination;
            myPlanet.currentHealth -= GameOptions.StandardFactoryContamination;
        }

        float efficiency = (myPlanet.currentHealth / 100.0f) * (1 + (myPlanet.PlanetId * GameOptions.PlanetEfficiencyIncrease));

        if (Time.time >= m_nextResourceTime)
        {
            m_nextResourceTime = Time.time + GameOptions.ResourceRefreshRate;
            if (EcoFactory)
                ResourcesManager.AddResources(Mathf.RoundToInt(GameOptions.EcoFactoryProduction * efficiency), myPlanet.PlanetId);
            else
                ResourcesManager.AddResources(Mathf.RoundToInt(GameOptions.StandardFactoryProduction * efficiency), myPlanet.PlanetId);
        }
    }
}
