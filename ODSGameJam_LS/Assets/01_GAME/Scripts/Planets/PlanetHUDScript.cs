using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHUDScript : MonoBehaviour
{
    [SerializeField]
    private Button PayButtonBtn, ColonizeButton;

    [SerializeField]
    private Text PayTimerText, CurrentResources, ResourcesToPay;

    [SerializeField]
    private Text ColonizedPlanetsText, PopUpText_Obj, planetNumber;

    [SerializeField]
    private GameObject NextPlanetBtn, PrevPlanetBtn, HealthIndicator, PolutionIndicator, planetNumberObj;

    private Image   healthImage;
    private Slider  healthSlider;
    private Text    healthText;

    private Text    PolutionText;
    private Slider  PolutionSlider;
    private Slider  PolutionSlider_Back;
    private Image   PolutionImage;
    private Image   PolutionImage_Back;

    //--- PopUpText vars ---
    private Text PopUpText;
    bool firstIteration = true;
    int lastSum = 0;
    int currSum;
    float LastSumTime;

    private void Start()
    {
        ColonizedPlanetsText.text = "Colonized Planets: " + GameManager.PlanetList.Count.ToString();

        healthImage         = HealthIndicator.GetComponentInChildren<Image>();
        healthSlider        = HealthIndicator.GetComponent<Slider>();
        healthText          = HealthIndicator.GetComponentInChildren<Text>();

        PolutionImage       = PolutionIndicator.GetComponentInChildren<Image>();
        PolutionImage_Back  = PolutionIndicator.GetComponentInChildren<Image>();

        PolutionSlider      = PolutionIndicator.GetComponent<Slider>();
        PolutionSlider_Back = PolutionIndicator.GetComponent<Slider>();
        PolutionText        = PolutionIndicator.GetComponentInChildren<Text>();

        PopUpText           = PopUpText_Obj.GetComponent<Text>();
        planetNumber        = planetNumberObj.GetComponent<Text>();

        InvokeRepeating("SumResources", 1.0f, 0.7f);
   
    }

    void SumResources()
    {    
        currSum = GameManager.CurrentResources - lastSum;
        if (GameManager.CurrentResources - lastSum > 0 )
        {
            PopUpText.color = new Color(PopUpText.color.r, PopUpText.color.g, PopUpText.color.b, 1);
            lastSum = GameManager.CurrentResources;
            PopUpText.enabled = true;
            PopUpText.text = "+" + currSum.ToString();
        }else if (GameManager.CurrentResources - lastSum < 0)
        {
            PopUpText.color = new Color(PopUpText.color.r, 0, 0, 1);
            lastSum = GameManager.CurrentResources;
            PopUpText.enabled = true;
            PopUpText.text = "-" + currSum.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // --- Pay Button ---
        if (GameManager.CurrentResources >= GameOptions.PayingAmount)
            PayButtonBtn.interactable = true;
        else
            PayButtonBtn.interactable = false;

        // --- Colonize Button ---
        if (GameManager.CurrentResources >= GameOptions.PlanetCost)
            ColonizeButton.interactable = true;
        else
            ColonizeButton.interactable = false;

        // --- Current Resources & Debt Text + Timer ---
        CurrentResources.text   = GameManager.CurrentResources.ToString();
        ResourcesToPay.text     = GameManager.CurrentDebt.ToString();
        PayTimerText.text       = ResourcesManager.DebtTimer.GetTimeString();

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

        // --- Polution Bar ---
        UpdatePolutionIndicator();

        // ---PopUpText ---
        PopUpText.color = new Color(PopUpText.color.r, PopUpText.color.g, PopUpText.color.b, PopUpText.color.a - 0.5f*Time.deltaTime);

        // ---planet number
        planetNumber.text = "planet: " + GameManager.CurrentPlanetIndex.ToString();
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
        if (GameManager.CurrentResources >= GameOptions.PlanetCost)
        {
            ResourcesManager.SubstractResources(GameOptions.PlanetCost);
            PlanetManager.CreatePlanet();
            ColonizedPlanetsText.text = "Colonized Planets: " + GameManager.PlanetList.Count.ToString();
        }
    }

    public void UpdateHealthIndicator()
    {
        float planetHealth = GameManager.PlanetList[GameManager.CurrentPlanetIndex].currentHealth;

        float h = planetHealth / 360;// h = 100 --> green ,  h =0 --> red

        healthImage.color = Color.HSVToRGB(h, 1.0f, 1.0f);
        healthText.color = Color.HSVToRGB(h, 1.0f, 1.0f);
        healthSlider.value = planetHealth / 100;

        healthText.text = "Health: " + planetHealth.ToString("0.##") + " %";
    }


    public void UpdatePolutionIndicator()
    {
        float planetPolution = GameManager.PlanetList[GameManager.CurrentPlanetIndex].currentPolution;

        float P = planetPolution / 360;

        PolutionImage.color = Color.HSVToRGB(P, 1.0f, 1.0f);
        PolutionImage_Back.color = Color.HSVToRGB(105.0f, 105.0f, 105.0f);
        PolutionText.color = Color.HSVToRGB(P, 1.0f, 1.0f);
        PolutionSlider.value = planetPolution / 100;
        PolutionSlider_Back.value = 100;

        PolutionText.text = "Polution: " + planetPolution.ToString("0.##") + " %";
    }
}
