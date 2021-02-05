using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/GameResults")]
public class GameResults : ScriptableObject
{
    private static GameResults m_instance;
    private static bool m_isInstanced = false;
    public static GameResults Instance {
        get {
            if (m_isInstanced) return m_instance;
            GameResults[] assets = Resources.LoadAll<GameResults>("");
            if (assets.Length > 1)
                Debug.LogError("Found multiple GameResults on the Resources folder, there should only be one!");
            if (assets.Length == 0)
            {
                m_instance = CreateInstance<GameResults>();
                Debug.LogError("Could not find a GameResults on Resources folder, one was created at runtime but it will not persist!");
            }
            else
                m_instance = assets[0];
            
            m_isInstanced = true;
            return m_instance;
        }
    }

    [SerializeField]
    private bool m_isVictory;
    public static bool IsVictory {
        get {
            return Instance.m_isVictory;
        }
        set {
            Instance.m_isVictory = value;
        }
    }

    [SerializeField]
    private int m_totalPlanets;
    public static int TotalPlanets {
        get {
            return Instance.m_totalPlanets;
        }
        set {
            Instance.m_totalPlanets = value;
        }
    }

    [SerializeField]
    private int m_deadPlanets;
    public static int DeadPlanets {
        get {
            return Instance.m_deadPlanets;
        }
        set {
            Instance.m_deadPlanets = value;
        }
    }

    [SerializeField]
    private int m_finalResources;
    public static int FinalResources {
        get {
            return Instance.m_finalResources;
        }
        set {
            Instance.m_finalResources = value;
        }
    }
}