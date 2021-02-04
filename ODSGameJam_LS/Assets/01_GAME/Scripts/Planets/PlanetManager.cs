using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : Singleton<PlanetManager>
{
    [SerializeField]
    int PosRange = 25;

    [SerializeField]
    private PlanetScript InitialPlanet;

    [SerializeField]
    private GameObject PlanetPrefab;

    [SerializeField]
    private GameObject CurrentCamera;

    // Start is called before the first frame update
    void Start()
    {
        //(GameManager.PlanetList).Add(InitialPlanet); //Sergi: Now done in Start of PlanetScript
        InitialPlanet.AttachCamera();
    }

    // Update is called once per frame
    void Update()
    {
        // --- Planet Creation ---
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector3 planet_pos = new Vector3(Random.Range(-PosRange, PosRange), Random.Range(-PosRange, PosRange), Random.Range(-PosRange, PosRange));
            Vector3 planet_rot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

            Instantiate(PlanetPrefab, planet_pos, Quaternion.Euler(planet_rot)).GetComponent<PlanetScript>();
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
        GameManager.PlanetList[GameManager.CurrentPlanetIndex].DetachCamera();

        GameObject next_planet;
        if (forward)
            next_planet = GameManager.NextPlanet().CameraPosition;
        else
            next_planet = GameManager.PreviousPlanet().CameraPosition;

        CurrentCamera.GetComponent<PlanetSwitchCameraScript>().SwitchPlanet(next_planet.transform);
    }
}
