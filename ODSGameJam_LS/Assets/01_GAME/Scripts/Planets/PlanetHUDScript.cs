using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHUDScript : MonoBehaviour
{
    private BackTimer m_PayTime;
    [SerializeField]
    private Button PayButtonBtn;

    [SerializeField]
    private Text PayTimerText, CurrentResources, ResourcesToPay;

    [SerializeField]
    private Text ColonizedPlanetsText;

    [SerializeField]
    private GameObject NextPlanetBtn, PrevPlanetBtn, HealthIndicator;

    private Image healthImage;
    private Slider healthSlider;
    private Text healthText;


    private void Start()
    {
        ColonizedPlanetsText.text = "Colonized Planets: " + GameManager.PlanetList.Count.ToString();

        m_PayTime = GetComponent<BackTimer>();
        m_PayTime.StartTime = GameOptions.TimerDuration;
        m_PayTime.Begin();

        healthImage =   HealthIndicator.GetComponentInChildren<Image>();
        healthSlider =  HealthIndicator.GetComponent<Slider>();
        healthText =    HealthIndicator.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // --- Pay Button ---
        if (GameManager.CurrentResources >= GameOptions.PayingAmount)
            PayButtonBtn.interactable = true;
        else
            PayButtonBtn.interactable = false;

        // --- Current Resources & Debt Text + Timer ---
        CurrentResources.text = GameManager.CurrentResources.ToString();
        ResourcesToPay.text = GameManager.CurrentDebt.ToString();
        PayTimerText.text = m_PayTime.GetTimeString();

        if (m_PayTime.Finished)
            m_PayTime.Begin();

        // --- Next/Prev Buttons ---
        if (!NextPlanetBtn.activeInHierarchy && !PrevPlanetBtn.activeInHierarchy)
        {
            if (GameManager.PlanetList.Count > 1)
            {
                NextPlanetBtn.SetActive(true);
                PrevPlanetBtn.SetActive(true);
            }
        }

        // --- Health Bar ---
        UpdateHealthIndicator();
    }

    public void PayButton()
    {
        if (GameManager.CurrentResources >= GameOptions.PayingAmount)
        {
            ResourcesManager.SubstractDebt(GameOptions.PayingAmount);
            ResourcesManager.SubstractResources(GameOptions.PayingAmount);
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
    public void UpdateHealthIndicator()
    {
        float planetHealth = GameManager.PlanetList[GameManager.CurrentPlanetIndex].currentHealth;

        float h = planetHealth / 360;// h = 100 --> green ,  h =0 --> red

        healthImage.color = Color.HSVToRGB(h, 1.0f, 1.0f);
        healthText.color = Color.HSVToRGB(h, 1.0f, 1.0f);
        healthSlider.value = planetHealth / 100;

        healthText.text = "Health: " + planetHealth.ToString() + " %";
    }
}
