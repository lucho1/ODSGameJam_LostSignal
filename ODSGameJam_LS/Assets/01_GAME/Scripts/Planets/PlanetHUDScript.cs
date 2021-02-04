using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHUDScript : MonoBehaviour
{
    private PlanetManager m_PlanetManager;

    [SerializeField]
    private Text ColonizedPlanetsText;

    [SerializeField]
    private GameObject NextPlanetBtn, PrevPlanetBtn;

    // Start is called before the first frame update
    void Start()
    {
        m_PlanetManager = GetComponentInParent<PlanetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.PlanetList.Count > 1)
        {
            NextPlanetBtn.SetActive(true);
            PrevPlanetBtn.SetActive(true);
        }
    }

    public void NextPlanetButton()
    {
        //m_PlanetManager.SwitchPlanet();
    }

    public void PrevPlanetButton()
    {
        //m_PlanetManager.SwitchPlanet(false);
    }

    public void ColonizePlanetButton()
    {
        ColonizedPlanetsText.text = "Colonized Planets: " + GameManager.PlanetList.Count.ToString();

    }
}
