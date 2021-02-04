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
        // --- Planet Switch ---
        if (GameManager.PlanetList.Count > 1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                SwitchPlanet();

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                SwitchPlanet(false);
        }
    }

    public static void CreatePlanet()
    {
        float pos_range = Instance.PosRange;
        Vector3 planet_pos = new Vector3(Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range), Random.Range(-pos_range, pos_range));
        Vector3 planet_rot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        Instantiate(Instance.PlanetPrefab, planet_pos, Quaternion.Euler(planet_rot)).GetComponent<PlanetScript>();
        SwitchPlanet();
    }

    public static void SwitchPlanet(bool forward = true)
    {
        if (GameManager.PlanetList.Count <= 1)
            return;

        GameManager.PlanetList[GameManager.CurrentPlanetIndex].DetachCamera();
        GameObject next_planet;

        if (forward)
            next_planet = GameManager.NextPlanet().CameraPosition;
        else
            next_planet = GameManager.PreviousPlanet().CameraPosition;

        Instance.CurrentCamera.GetComponent<PlanetSwitchCameraScript>().SwitchPlanet(next_planet.transform);
    }
}
