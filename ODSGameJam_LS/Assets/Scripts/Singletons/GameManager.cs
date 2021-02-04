using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() {}
    
    private List<PlanetScript> m_planetList;
    public static List<PlanetScript> PlanetList {
        get {
            return Instance.m_planetList;
        }
        set {
            Instance.m_planetList = value;
        }
    }

    private int m_currentResources;
    public static int CurrentResources {
        get {
            return Instance.m_currentResources;
        }
        set {
            Instance.m_currentResources = value;
        }
    }
}
