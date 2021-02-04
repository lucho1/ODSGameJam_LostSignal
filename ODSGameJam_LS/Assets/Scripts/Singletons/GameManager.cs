using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public enum TypeOfConstruction {
        Factory = 0,
        EcoFactory,
        Destroy
    }

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

    private TypeOfConstruction m_selectedConstruction = TypeOfConstruction.Factory;
    public static TypeOfConstruction SelectedConstruction {
        get {
            return Instance.m_selectedConstruction;
        }
        set {
            Instance.m_selectedConstruction = value;
        }
    }

    private int m_currentPlanetIndex;
    public static int CurrentPlanetIndex {
        get {
            return Instance.m_currentPlanetIndex;
        }
        set {
            Instance.m_currentPlanetIndex = value;
        }
    }

    public static PlanetScript NextPlanet() {
        if (CurrentPlanetIndex == PlanetList.Count - 1)
            CurrentPlanetIndex = 0;
        else
            ++CurrentPlanetIndex;

        return PlanetList[CurrentPlanetIndex];
    }

    public static PlanetScript PreviousPlanet() {
        if (CurrentPlanetIndex == 0)
            CurrentPlanetIndex = PlanetList.Count - 1;
        else
            --CurrentPlanetIndex;

        return PlanetList[CurrentPlanetIndex];
    }
}
