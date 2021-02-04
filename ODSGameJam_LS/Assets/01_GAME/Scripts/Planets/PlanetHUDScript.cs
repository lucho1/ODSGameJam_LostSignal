using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHUDScript : MonoBehaviour
{
    [SerializeField]
    private Text ColonizedPlanetsText;

    [SerializeField]
    private GameObject NextPlanetBtn, PrevPlanetBtn;

    private void Start()
    {
        ColonizedPlanetsText.text = "Colonized Planets: " + GameManager.PlanetList.Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!NextPlanetBtn.activeInHierarchy && !PrevPlanetBtn.activeInHierarchy)
        {
            if (GameManager.PlanetList.Count > 1)
            {
                NextPlanetBtn.SetActive(true);
                PrevPlanetBtn.SetActive(true);
            }
        }
    }

    public void NextPlanetButton()
    {
        PlanetManager.SwitchPlanet();
    }

    public void PrevPlanetButton()
    {
        PlanetManager.SwitchPlanet(false);
    }

    public void ColonizePlanetButton()
    {
        PlanetManager.CreatePlanet();
        ColonizedPlanetsText.text = "Colonized Planets: " + GameManager.PlanetList.Count.ToString();
    }
}
