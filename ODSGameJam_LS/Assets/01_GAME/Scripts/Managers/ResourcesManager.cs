
/*

This script is suposed to manage global data: Resources for example 
 

This script needs to have all GObjs that are part of the UI
*/


public class ResourcesManager : Singleton<ResourcesManager>
{

    private SimpleTimer m_debtTimer;
    public static SimpleTimer DebtTimer {
        get {
            return Instance.m_debtTimer;
        }
        set {
            Instance.m_debtTimer = value;
        }
    }

    // Start is called before the first frame update
    private int resources;
    public static int Resources {
        get {
            return Instance.resources;
        }
    }

    void Start()
    {
        DebtTimer = new SimpleTimer();
        DebtTimer.Duration = GameOptions.TimerDuration;
        DebtTimer.Begin();
        Instance.resources = GameManager.CurrentResources = GameOptions.StartingFunds;
        GameManager.CurrentDebt = GameOptions.OwedResources;
        InvokeRepeating("UpdateResources", 1.0f, GameOptions.RefreshRate);
    }

    void UpdateResources() //called every x frames
    {
        GameManager.CurrentResources = Instance.resources;
        if (DebtTimer.Finished()) {
            AddDebt(GameOptions.IncreasedDebt);
            DebtTimer.Begin();
        }
    }


    // USER FUNCTIONS
    public static void AddResources(int amount,int planet_id) //called from other scripts
    {
        Instance.resources = Instance.resources + amount;
        GameManager.PlanetList[planet_id].UiScript.PopChanges(amount);
    }

    public static void SubstractResources(int amount) //called from other scripts
    {
        UISoundsManager.PlaySound(UISoundsManager.PaySound);
        Instance.resources = Instance.resources - amount;
    }

    public static void SubstractDebt(int debt_amount)
    {
        GameManager.CurrentDebt -= debt_amount;
        if (GameManager.CurrentDebt <= 0)
            GameManager.FinishGame(true);
    }

    public static void AddDebt(int debt_amount)
    {
        UISoundsManager.PlaySound(UISoundsManager.DebtIncreasesSound);
        GameManager.CurrentDebt += debt_amount;
    }
}
