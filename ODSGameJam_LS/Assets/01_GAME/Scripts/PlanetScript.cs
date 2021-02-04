using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    public GameObject CameraPosition;

    private float m_PlanetContamination = 0;

    public void AddContamination(float quantity)
    {
        m_PlanetContamination += quantity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
