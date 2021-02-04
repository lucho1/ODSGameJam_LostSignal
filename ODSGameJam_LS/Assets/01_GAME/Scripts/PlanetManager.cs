using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField]
    private PlanetScript InitialPlanet;

    [SerializeField]
    private GameObject PlanetPrefab;

    [SerializeField]
    private GameObject CurrentCamera;

    // Start is called before the first frame update
    void Start()
    {
        (GameManager.PlanetList).Add(InitialPlanet);
    }

    // Update is called once per frame
    void Update()
    {
        // --- Planet Creation ---
        if (Input.GetKeyDown(KeyCode.B))
        {
            (GameManager.PlanetList).Add(Instantiate(PlanetPrefab).GetComponent<PlanetScript>());
            SwitchPlanet();
        }


        // --- Planet Switch ---
        if (GameManager.PlanetList.Count > 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SwitchPlanet();

            if (Input.GetKeyDown(KeyCode.LeftControl))
                SwitchPlanet(false);
        }
    }

    void SwitchPlanet(bool forward = true)
    {
        GameObject next_planet;
        if (forward)
            next_planet = GameManager.NextPlanet().CameraPosition;
        else
            next_planet = GameManager.PreviousPlanet().CameraPosition;

        CurrentCamera.GetComponent<PlanetSwitchCameraScript>().SwitchPlanet(next_planet.transform);
    }
}
