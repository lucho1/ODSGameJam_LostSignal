using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

This script is suposed to manage global data: Resources for example 
 

This script needs to have all GObjs that are part of the UI
*/


public class ResourcesManager : Singleton<ResourcesManager>
{

    // Start is called before the first frame update
    private int resources;
    

    public ResourcesUI PopUpScoreUIGameObject;
    public float refreshRate = 0.5f;

    void Start()
    {
        InvokeRepeating("UpdateResources", 1.0f, refreshRate);
        resources = GameManager.CurrentResources;
   
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AddResources(110);
        }
    }

    void UpdateResources() //called every x frames
    {
        GameManager.CurrentResources = resources;
    }


    //USER FUNCTIONS

    static void AddResources(int amount) //called from other scripts
    {
        Instance.resources = Instance.resources + amount;

        Instance.PopUpScoreUIGameObject.DisplayNumber = amount;
        Instance.PopUpScoreUIGameObject.changes = true;
        
    }
    static void SubstractResources(int amount) //called from other scripts
    {
        Instance.resources = Instance.resources - amount;
    }



}
