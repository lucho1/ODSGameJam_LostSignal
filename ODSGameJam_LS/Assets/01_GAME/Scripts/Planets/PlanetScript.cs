using UnityEngine;
using UnityEngine.Events;

public class PlanetScript : MonoBehaviour
{

    public ResourcesUI UiScript;

    [Space(10)]
    [Header("Planet atributes")]

    public int planetHealthRecoveryRate = 5;    //Health recovered per second
    public float currentHealth = 100.0f;             //Planet's health from 0 to 100
    public float currentPolution = 0.0f;
    public bool planetAlive = true;


    //Events
    public UnityEvent planetDeath;

    //Timers
    private SimpleTimer healthRegenTimer;
    public float resourceUpdatePeriod = 1.0f;   //Every x seconds the resources produced by this planet will be updated on the resource manager
    private float lastResourceUpdateTime = 0.0f;
    private int resourcesIncome = 0;

    [Space(10)]
    [Header("Rotation atribute")]
    public float rotationSpeed = 5.0f;
    public float deaceleration = 0.4f;
    private bool rotatingPlanet = false;
    private Vector2 currentVelocity = new Vector2(0, 0);
    public float wasdVelocity = 0.4f;
    public float zoomVelocity = 50.0f;
    public float zoomDeaceleration = 2.8f;
    private float zoomNormVelocity=0.0f;
    private float minCameraDistane;
    private float maxCameraDistane;

    [Space(10)]
    public Camera camera;
    public GameObject CameraPosition;
    private bool cameraAttached = false;
    public ParticleSystem m_ContaminationFog;

    [System.NonSerialized]
    public int FactoriesSpawned = 0;
    [System.NonSerialized]
    public int PlanetId;
    [System.NonSerialized]
    public int MaxGround;
    [System.NonSerialized]
    public int GroundSpawned;

    private void Awake()
    {
        MaxGround = Random.Range(5, 8);
        PlanetId = GameManager.PlanetList.Count;
        GameManager.PlanetList.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        healthRegenTimer = new SimpleTimer();
        healthRegenTimer.Duration = GameOptions.PlanetRegenerationRefresh;
        healthRegenTimer.Begin();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        minCameraDistane = (transform.position - CameraPosition.transform.position).magnitude*0.75f;
        maxCameraDistane = (transform.position - CameraPosition.transform.position).magnitude * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {     

        if (planetAlive)
            UpdateHealth();

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
            HoldWASDInput();
            HoldCameraZoom();
            if (!rotatingPlanet)
                UpdateSpeed();
        }
    }

    private void UpdateHealth() 
    {
        if (currentHealth > 0) {
            currentHealth -= currentPolution;
            currentPolution = 0;
            if (healthRegenTimer.Finished()) {
                healthRegenTimer.Begin();
                currentHealth += GameOptions.PlanetRegeneration;
                if (currentHealth > 100)
                    currentHealth = 100;
            }
        }
        if (currentHealth <= 0) {
            currentHealth = 0;
            planetAlive = false;
            planetDeath.Invoke();
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
    }

    public void DetachCamera()
    {
        cameraAttached = false;
    }

   public void HoldWASDInput()
    {
        //Horizontal velocity
        if (Input.GetKey(KeyCode.A) == true && Input.GetKey(KeyCode.D) == false)
            currentVelocity.x = -wasdVelocity;
        else if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == true)
            currentVelocity.x = wasdVelocity;

        //Vertical velocity
        if (Input.GetKey(KeyCode.W) == true && Input.GetKey(KeyCode.S) == false)
            currentVelocity.y = wasdVelocity;
        else if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == true)
            currentVelocity.y = -wasdVelocity;
    }

    public void HoldCameraZoom()
    {
        //Hold mousewheel input
        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel"))*10 > Mathf.Abs(zoomNormVelocity))
            zoomNormVelocity= Input.GetAxis("Mouse ScrollWheel")*10/*don't blame me, blame unity*/;


        if ((transform.position - camera.transform.position).magnitude > minCameraDistane && zoomNormVelocity > 0 ||
            (transform.position - camera.transform.position).magnitude < maxCameraDistane && zoomNormVelocity < 0)
            camera.gameObject.transform.position += camera.gameObject.transform.forward * zoomNormVelocity * zoomVelocity * Time.deltaTime;

        //Reduce velocity depending on the deaceleration

        if (zoomNormVelocity > 0)
        {
            zoomNormVelocity -= zoomDeaceleration * Time.deltaTime;
            if (zoomNormVelocity < 0)
                zoomNormVelocity = 0.0f;
        }
        else if (zoomNormVelocity < 0)
        {
            zoomNormVelocity += zoomDeaceleration * Time.deltaTime;
            if (zoomNormVelocity > 0)
                zoomNormVelocity = 0.0f;
        }
    }

    public void AddFactory() {
        ++FactoriesSpawned;
        float Alpha = (FactoriesSpawned / 12.0f);
        var main = m_ContaminationFog.main;
        Color myColor = main.startColor.color;
        myColor.a = Alpha;
        main.startColor = myColor;
    }

    public void RemoveFactory() {
        --FactoriesSpawned;
        
        float Alpha = FactoriesSpawned == 0 ? 0 : (FactoriesSpawned / 12.0f);
        var main = m_ContaminationFog.main;
        Color myColor = main.startColor.color;
        myColor.a = Alpha;
        main.startColor = myColor;
    }
}
