
/*

This script is suposed to manage global data: Resources for example 
 

This script needs to have all GObjs that are part of the UI
*/


public class ResourcesManager : Singleton<ResourcesManager>
{

    // Start is called before the first frame update
    private int resources;
    public static int Resources {
        get {
            return Instance.resources;
        }
    }

    void Start()
    {
        Instance.resources = GameManager.CurrentResources = GameOptions.StartingFunds;
        GameManager.CurrentDebt = GameOptions.OwedResources;
        InvokeRepeating("UpdateResources", 1.0f, GameOptions.RefreshRate);
    }

    void UpdateResources() //called every x frames
    {
        GameManager.CurrentResources = Instance.resources;
    }


    // USER FUNCTIONS
    public static void AddResources(int amount,int planet_id) //called from other scripts
    {
        Instance.resources = Instance.resources + amount;
        GameManager.PlanetList[planet_id].UiScript.PopChanges(amount);
    }

    public static void SubstractResources(int amount) //called from other scripts
    {
        Instance.resources = Instance.resources - amount;
    }
}
