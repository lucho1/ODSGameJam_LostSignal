using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

This script is suposed to manage global data: Resources for example 
 
*/


public class ResourcesManager : Singleton<ResourcesManager>
{

    // Start is called before the first frame update
    int resources;
    int ammountOfPlanets;

    void Start()
    {
        InvokeRepeating("UpdateResources", 1.0f, 0.5f);
        resources = GameManager.CurrentResources;
        if (GameManager.PlanetList.Capacity > 0)
        {
            ammountOfPlanets = GameManager.PlanetList.Count;
        }  
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateResources() //called every x frames
    {
        GameManager.CurrentResources = resources;
    }


    //USER FUNCTIONS

    static void AddResources(int ammount) //called from other scripts
    {
        Instance.resources = Instance.resources + ammount;
    }
    static void SubstractResources(int ammount) //called from other scripts
    {
        Instance.resources = Instance.resources - ammount;
    }



}
