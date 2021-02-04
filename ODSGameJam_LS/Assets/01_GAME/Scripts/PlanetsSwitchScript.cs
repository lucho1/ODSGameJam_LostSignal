using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsSwitchScript : MonoBehaviour
{
    [SerializeField]
    private List<PlanetScript> PlanetList = new List<PlanetScript>();

    [SerializeField]
    private List<GameObject> PlanetListCameras = new List<GameObject>();

    [SerializeField]
    private GameObject CurrentCamera;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.PlanetList = PlanetList;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.NextPlanet();
            Transform newPos = PlanetListCameras[GameManager.CurrentPlanetIndex].transform;
            CurrentCamera.GetComponent<PlanetSwitchCameraScript>().SwitchPlanet(newPos);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameManager.PreviousPlanet();
            Transform newPos = PlanetListCameras[GameManager.CurrentPlanetIndex].transform;
            CurrentCamera.GetComponent<PlanetSwitchCameraScript>().SwitchPlanet(newPos);
        }
    }
}
