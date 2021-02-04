using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsSwitchScript : MonoBehaviour
{
    [SerializeField]
    private PlanetScript PlanetPrefab;

    [SerializeField]
    private GameObject CurrentCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Transform camPos = PlanetPrefab.CameraPosition.transform;
            CurrentCamera.GetComponent<PlanetSwitchCameraScript>().SwitchPlanet(camPos);
        }
    }
}
