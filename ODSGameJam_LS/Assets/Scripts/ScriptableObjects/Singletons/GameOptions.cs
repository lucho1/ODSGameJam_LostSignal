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
    private float m_planetCostIncrease;
    public static float PlanetCostIncrease {
        get {
            return Instance.m_planetCostIncrease;
        }
        set {
            Instance.m_planetCostIncrease = value;
        }
    }
}
