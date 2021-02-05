using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/GameOptions")]
public class GameOptions : ScriptableObject
{
    private static GameOptions m_instance;
    private static bool m_isInstanced = false;
    public static GameOptions Instance {
        get {
            if (m_isInstanced) return m_instance;
            GameOptions[] assets = Resources.LoadAll<GameOptions>("");
            if (assets.Length > 1)
                Debug.LogError("Found multiple GameOptions on the Resources folder, there should only be one!");
            if (assets.Length == 0)
            {
                m_instance = CreateInstance<GameOptions>();
                Debug.LogError("Could not find a GameOptions on Resources folder, one was created at runtime but it will not persist!");
            }
            else
                m_instance = assets[0];
            
            m_isInstanced = true;
            return m_instance;
        }
    }

    [SerializeField]
    private int m_startingFunds;
    public static int StartingFunds {
        get {
            return Instance.m_startingFunds;
        }
        set {
            Instance.m_startingFunds = value;
        }
    }

    [SerializeField]
    private int m_owedResources;
    public static int OwedResources {
        get {
            return Instance.m_owedResources;
        }
        set {
            Instance.m_owedResources = value;
        }
    }

    [SerializeField]
    private float m_timerDuration;
    public static float TimerDuration {
        get {
            return Instance.m_timerDuration;
        }
        set {
            Instance.m_timerDuration = value;
        }
    }

    [SerializeField]
    private int m_increasedDebt;
    public static int IncreasedDebt {
        get {
            return Instance.m_increasedDebt;
        }
        set {
            Instance.m_increasedDebt = value;
        }
    }

    [SerializeField]
    private int m_payingAmount;
    public static int PayingAmount {
        get {
            return Instance.m_payingAmount;
        }
        set {
            Instance.m_payingAmount = value;
        }
    }

    [SerializeField]
    private int m_standardFactoryCost;
    public static int StandardFactoryCost {
        get {
            return Instance.m_standardFactoryCost;
        }
        set {
            Instance.m_standardFactoryCost = value;
        }
    }

    [SerializeField]
    private int m_ecoFactoryCost;
    public static int EcoFactoryCost {
        get {
            return Instance.m_ecoFactoryCost;
        }
        set {
            Instance.m_ecoFactoryCost = value;
        }
    }

    [SerializeField]
    private int m_destructionCost;
    public static int DestructionCost {
        get {
            return Instance.m_destructionCost;
        }
        set {
            Instance.m_destructionCost = value;
        }
    }

    [SerializeField]
    private int m_planetCost;
    public static int PlanetCost {
        get {
            return Instance.m_planetCost;
        }
        set {
            Instance.m_planetCost = value;
        }
    }

    [SerializeField]
    private float m_planetCostIncrease;
    public static float PlanetCostIncrease {
        get {
            return Instance.m_planetCostIncrease;
        }
        set {
            Instance.m_planetCostIncrease = value;
        }
    }

    [SerializeField]
    private float m_planetEfficiencyIncrease;
    public static float PlanetEfficiencyIncrease {
        get {
            return Instance.m_planetEfficiencyIncrease;
        }
        set {
            Instance.m_planetEfficiencyIncrease = value;
        }
    }

    [SerializeField]
    private float m_standardFactoryContamination;
    public static float StandardFactoryContamination {
        get {
            return Instance.m_standardFactoryContamination;
        }
        set {
            Instance.m_standardFactoryContamination = value;
        }
    }

    [SerializeField]
    private float m_ecoFactoryContamination;
    public static float EcoFactoryContamination {
        get {
            return Instance.m_ecoFactoryContamination;
        }
        set {
            Instance.m_ecoFactoryContamination = value;
        }
    }

    [SerializeField]
    private float m_planetRegeneration;
    public static float PlanetRegeneration {
        get {
            return Instance.m_planetRegeneration;
        }
        set {
            Instance.m_planetRegeneration = value;
        }
    }

    [SerializeField]
    private int m_planetRegenationRefresh;
    public static int PlanetRegenerationRefresh {
        get {
            return Instance.m_planetRegenationRefresh;
        }
        set {
            Instance.m_planetRegenationRefresh = value;
        }
    }


    [SerializeField]
    private float m_refreshRate;
    public static float RefreshRate {
        get {
            return Instance.m_refreshRate;
        }
        set {
            Instance.m_refreshRate = value;
        }
    }

    [SerializeField]
    private float m_resourceRefreshRate;
    public static float ResourceRefreshRate {
        get {
            return Instance.m_resourceRefreshRate;
        }
        set {
            Instance.m_resourceRefreshRate = value;
        }
    }

    [SerializeField]
    private float m_pollutionRefreshRate;
    public static float PollutionRefreshRate {
        get {
            return Instance.m_pollutionRefreshRate;
        }
        set {
            Instance.m_pollutionRefreshRate = value;
        }
    }

    [SerializeField]
    private int m_ecoFactoryProduction;
    public static int EcoFactoryProduction {
        get {
            return Instance.m_ecoFactoryProduction;
        }
        set {
            Instance.m_ecoFactoryProduction = value;
        }
    }

    [SerializeField]
    private int m_standardFactoryProduction;
    public static int StandardFactoryProduction {
        get {
            return Instance.m_standardFactoryProduction;
        }
        set {
            Instance.m_standardFactoryProduction = value;
        }
    }
}
