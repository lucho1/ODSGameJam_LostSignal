using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [Space(10)] [Header("Planet atributes")]
    
    public int planetHealthRecoveryRate = 5;    //Health recovered per second
    public int currentHealth =100;              //Planet's health from 0 to 100
    
    //Timers
    public float resourceUpdatePeriod = 1.0f;   //Every x seconds the resources produced by this planet will be updated on the resource manager
    private float lastResourceUpdateTime = 0.0f;
    private int resourcesIncome = 0;

    [Space(10)] [Header("Rotation atribute")]
    public float rotationSpeed=5.0f;
    public float deaceleration = 0.4f;
    private bool rotatingPlanet = false;
    private Vector2 currentVelocity = new Vector2(0,0);

    [Space(10)]
    public Camera camera;

    private bool cameraAttached = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastResourceUpdateTime > resourceUpdatePeriod)
        {
            lastResourceUpdateTime = Time.time;
            //Update resources on the thing
        }

        //update camera rotation if the planet is being looked at
        if (cameraAttached)
        {
            if (Input.GetMouseButtonDown(0))
                rotatingPlanet = true;
            if (Input.GetMouseButtonUp(0))
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
        camera.gameObject.transform.RotateAround(gameObject.transform.position,camera.transform.up, currentVelocity.x * rotationSpeed);
        camera.gameObject.transform.RotateAround(gameObject.transform.position, camera.transform.right, -currentVelocity.y * rotationSpeed);
    }

    //Reduce speed
    private void UpdateSpeed()
    {
        //X axis
        if (currentVelocity.x > 0.0001f)
        {
            currentVelocity.x = currentVelocity.x- deaceleration*Time.deltaTime;
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
    }

    public void DetachCamera()
    {
        cameraAttached = false;
    }
}
