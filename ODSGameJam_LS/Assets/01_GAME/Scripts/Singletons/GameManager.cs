using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum TypeOfConstruction {
        None = 0,
        Factory,
        EcoFactory,
        Destroy
    }

    protected GameManager() {}

    [SerializeField]
    private List<PlanetScript> m_planetList = new List<PlanetScript>();
    public static List<PlanetScript> PlanetList {
        get {
            return Instance.m_planetList;
        }
        set {
            Instance.m_planetList = value;
        }
    }

    [SerializeField]
    private int m_currentResources = 0;
    public static int CurrentResources {
        get {
            return Instance.m_currentResources;
        }
        set {
            Instance.m_currentResources = value;
        }
    }

    [SerializeField]
    private TypeOfConstruction m_selectedConstruction = TypeOfConstruction.None;
    public static TypeOfConstruction SelectedConstruction {
        get {
            return Instance.m_selectedConstruction;
        }
        set {
            Instance.m_selectedConstruction = value;
        }
    }

    [SerializeField]
    private int m_currentPlanetIndex = 0;
    public static int CurrentPlanetIndex {
        get {
            return Instance.m_currentPlanetIndex;
        }
        set {
            Instance.m_currentPlanetIndex = value;
        }
    }

    [SerializeField]
    private int m_currentDebt = GameOptions.OwedResources;
    public static int CurrentDebt {
        get {
            return Instance.m_currentDebt;
        }
        set {
            Instance.m_currentDebt = value;
        }
    }

    [SerializeField]
    private GameObject m_CurrentSelectedCell = null;
    public static GameObject CurrentSelectedCell {
        get {
            return Instance.m_CurrentSelectedCell;
        }
        set {
            Instance.m_CurrentSelectedCell = value;
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
