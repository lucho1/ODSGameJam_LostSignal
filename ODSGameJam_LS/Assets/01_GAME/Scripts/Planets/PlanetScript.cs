using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetScript : MonoBehaviour
{

    public ResourcesUI UiScript;

    [Space(10)]
    [Header("Planet atributes")]

    public int planetHealthRecoveryRate = 5;    //Health recovered per second
    public float currentHealth = 100.0f;             //Planet's health from 0 to 100
    public float currentPolution = 0.0f;
     

    //Timers
    public float resourceUpdatePeriod = 1.0f;   //Every x seconds the resources produced by this planet will be updated on the resource manager
    private float lastResourceUpdateTime = 0.0f;
    private int resourcesIncome = 0;

    [Space(10)]
    [Header("Rotation atribute")]
    public float rotationSpeed = 5.0f;
    public float deaceleration = 0.4f;
    private bool rotatingPlanet = false;
    private Vector2 currentVelocity = new Vector2(0, 0);

    [Space(10)]
    public Camera camera;
    public GameObject CameraPosition;
    private bool cameraAttached = false;

    [System.NonSerialized]
    public int PlanetId;
    [System.NonSerialized]
    public int MaxGround;
    [System.NonSerialized]
    public int GroundSpawned;

    //UI
    //public GameObject healthIndicator;
    //private Image healthImage;
    //private Slider healthSlider;
    //private Text healthText;

    private void Awake()
    {
        MaxGround = Random.Range(5, 8);
        PlanetId = GameManager.PlanetList.Count;
        GameManager.PlanetList.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //healthImage = healthIndicator.GetComponentInChildren<Image>();
        //healthSlider = healthIndicator.GetComponent<Slider>();
        //healthText = healthIndicator.GetComponentInChildren<Text>();

        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateHealthIndicator();

        if (Time.time - lastResourceUpdateTime > resourceUpdatePeriod)
        {
            lastResourceUpdateTime = Time.time;
            //Update resources on the thing
        }

        //update camera rotation if the planet is being looked at
        if (cameraAttached)
        {
            if (Input.GetMouseButtonDown(1))
                rotatingPlanet = true;
            if (Input.GetMouseButtonUp(1))
                rotatingPlanet = false;

            if (rotatingPlanet)
            {
                currentVelocity.x = Input.GetAxis("Mouse X");
                currentVelocity.y = Input.GetAxis("Mouse Y");
            }
            RotatePlanet();

            if (!rotatingPlanet)
                UpdateSpeed();
        }
    }

    private void RotatePlanet()
    {
        camera.gameObject.transform.RotateAround(gameObject.transform.position, camera.transform.up, currentVelocity.x * rotationSpeed);
        camera.gameObject.transform.RotateAround(gameObject.transform.position, camera.transform.right, -currentVelocity.y * rotationSpeed);
    }

    //Reduce speed
    private void UpdateSpeed()
    {
        //X axis
        if (currentVelocity.x > 0.0001f)
        {
            currentVelocity.x = currentVelocity.x - deaceleration * Time.deltaTime;
            Debug.Log(currentVelocity.x);
            if (currentVelocity.x < 0.0001f)
                currentVelocity.x = 0.0000f;
        }
        else if (currentVelocity.x < 0.0001f)
        {
            currentVelocity.x = currentVelocity.x + deaceleration * Time.deltaTime;
            if (currentVelocity.x > 0.0001f)
                currentVelocity.x = 0.0001f;
        }

        //Y axis
        if (currentVelocity.y > 0.0001f)
        {
            currentVelocity.y = currentVelocity.y - deaceleration * Time.deltaTime;
            if (currentVelocity.y < 0.0001f)
                currentVelocity.y = 0.0001f;
        }
        else if (currentVelocity.y < 0.0001f)
        {
            currentVelocity.y = currentVelocity.y + deaceleration * Time.deltaTime;
            if (currentVelocity.y > 0.0001f)
                currentVelocity.y = 0.0001f;
        }
    }

    public void UpdateIncome(int aditionalIncome)
    {
        resourcesIncome = resourcesIncome + aditionalIncome;
        if (resourcesIncome < 0)
            resourcesIncome = 0;
    }

    public void AttachCamera()
    {
        cameraAttached = true;
        //healthIndicator.SetActive(true);
    }

    public void DetachCamera()
    {
        cameraAttached = false;
        //healthIndicator.SetActive(false);
    }

    //public void UpdateHealthIndicator()
    //{
    //    float h = currentHealth / 360;// h = 100 --> green ,  h =0 --> red
      
    //    healthImage.color = Color.HSVToRGB(h,1.0f,1.0f);
    //    healthSlider.value = currentHealth / 100;

    //    healthText.text = "Health: " + currentHealth.ToString() + " %";
    //}
}
