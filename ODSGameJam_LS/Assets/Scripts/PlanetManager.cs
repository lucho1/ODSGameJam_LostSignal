using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public float rotationSpeed=10.0f;

    private bool rotatingPlanet = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            rotatingPlanet = true;
        if (Input.GetMouseButtonUp(0)) 
            rotatingPlanet = false;
        
        if (rotatingPlanet)
            RotatePlanet();
    }

    private void RotatePlanet()
    {
        gameObject.transform.Rotate(new Vector3(0, 1, 0), -Input.GetAxis("Mouse X")* rotationSpeed, Space.World);
        gameObject.transform.Rotate(new Vector3(1, 0, 0), Input.GetAxis("Mouse Y") * rotationSpeed, Space.World);

    }
}
