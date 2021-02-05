using UnityEngine;

public class CellSelector : MonoBehaviour
{
    enum TypeOfCell {
        Ground_1 = 0,
        Ground_2,
        Ground_3,
        Ground_4
    };

    public GameObject myOutline;
    public GameObject GroundPrefab1;
    public GameObject GroundPrefab2;
    public GameObject GroundPrefab3;
    public GameObject GroundPrefab4;
    public Transform Rotation;

    [TagSelector]
    public string TreeTag;
    [TagSelector]
    public string FactoryTag;
    [TagSelector]
    public string EcoTag;

    TypeOfCell type;
    bool isWater;
    bool isAlive = true;
    PlanetScript m_Planet;

    GameObject m_islandObject;
    Animation m_islandAnimation;
    GameObject m_treesObject;
    GameObject m_factoryObject;
    GameObject m_ecoObject;

    GameManager.TypeOfConstruction Construction = GameManager.TypeOfConstruction.None;

    // Start is called before the first frame update
    void Start() {
        m_Planet = GetComponentInParent<PlanetScript>();
        m_Planet.planetDeath.AddListener(OnPlanetDeath);

        type = (TypeOfCell)Random.Range(0,4);

        if (m_Planet && m_Planet.GroundSpawned < m_Planet.MaxGround)
        {
            float Throw = Random.Range(0.0f,100.0f);
            if ((m_Planet.GroundSpawned + 1) % 2 == 0)
                isWater = Throw > 20.0f;
            else
                isWater = Throw > 80.0f;

            if (!isWater)
                ++m_Planet.GroundSpawned;
        }
        else
            isWater = true;

        switch (type) {
            case TypeOfCell.Ground_1:
                m_islandObject = Instantiate(GroundPrefab1, transform.position, Rotation.rotation, transform);
            break;
            case TypeOfCell.Ground_2:
                m_islandObject = Instantiate(GroundPrefab2, transform.position, Rotation.rotation, transform);
            break;
            case TypeOfCell.Ground_3:
                m_islandObject = Instantiate(GroundPrefab3, transform.position, Rotation.rotation, transform);
            break;
            case TypeOfCell.Ground_4:
                m_islandObject = Instantiate(GroundPrefab4, transform.position, Rotation.rotation, transform);
            break;
        }

        SetupPrefab();
    }

    void OnMouseEnter() {
        // myTODO: Trigger outline here
        if (myOutline)
            myOutline.SetActive(true);
        GameManager.CurrentSelectedCell = this.gameObject;
    }

    void OnMouseExit() {
        // myTODO: Untrigger outline here
        if (myOutline)
            myOutline.SetActive(false);
        if (GameManager.CurrentSelectedCell == this.gameObject)
            GameManager.CurrentSelectedCell = null;
    }

    void OnMouseDown() {
        if (!isAlive) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            return;
        }

        switch (GameManager.SelectedConstruction) {
            default:
                SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
                break;
            case GameManager.TypeOfConstruction.Destroy:
                DestroyBuiltFactory();
                break;
            case GameManager.TypeOfConstruction.Factory:
                BuildFactory();
                break;
            case GameManager.TypeOfConstruction.EcoFactory:
                BuildEcoFactory();
                break;

        }
    }

    void SetupPrefab() {
        if (!m_islandObject)
            return;

        m_islandAnimation = m_islandObject.GetComponent<Animation>();
        Transform[] children = m_islandObject.GetComponentsInChildren<Transform>();
        foreach (Transform c in children) {
            if (c.CompareTag(TreeTag))
                m_treesObject = c.gameObject;
            else if (c.CompareTag(FactoryTag))
                m_factoryObject = c.gameObject;
            else if (c.CompareTag(EcoTag))
                m_ecoObject = c.gameObject;
        }

        if (m_factoryObject)
            m_factoryObject.SetActive(false);
        if (m_ecoObject)
            m_ecoObject.SetActive(false);

        if (isWater) {
            if (m_treesObject)
                m_treesObject.SetActive(false);
            
            m_islandObject.SetActive(false);
        }
    }

    void DestroyBuiltFactory() {
        if (Construction == GameManager.TypeOfConstruction.None) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            return;
        }
        if (GameManager.CurrentResources < GameOptions.DestructionCost) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            return;
        }

        ResourcesManager.SubstractResources(GameOptions.DestructionCost);
        
        m_ecoObject.SetActive(false);
        m_factoryObject.SetActive(false);

        if (isWater) {
            m_islandObject.SetActive(false);
        }
        else {
            m_treesObject.SetActive(true);
            m_islandAnimation.Play();
        }
        Construction = GameManager.TypeOfConstruction.None;

        SoundsManager.PlaySound(SoundsManager.TreeSound);
    }

    void BuildFactory() {
        if (Construction == GameManager.TypeOfConstruction.Factory) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            return;
        }
        if (GameManager.CurrentResources < GameOptions.StandardFactoryCost) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            return;
        }

        ResourcesManager.SubstractResources(GameOptions.StandardFactoryCost);

        m_islandObject.SetActive(true);
        m_ecoObject.SetActive(false);
        m_treesObject.SetActive(false);
        m_factoryObject.SetActive(true);
        Construction = GameManager.TypeOfConstruction.Factory;
        m_islandAnimation.Play();
        SoundsManager.PlaySound(SoundsManager.ConstructionSound);
    }

    void BuildEcoFactory() {
        if (Construction == GameManager.TypeOfConstruction.EcoFactory) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            return;
        }
        if (GameManager.CurrentResources < GameOptions.EcoFactoryCost) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            return;
        }

        ResourcesManager.SubstractResources(GameOptions.EcoFactoryCost);

        m_islandObject.SetActive(true);
        m_factoryObject.SetActive(false);
        m_treesObject.SetActive(true);
        m_ecoObject.SetActive(true);
        Construction = GameManager.TypeOfConstruction.EcoFactory;
        m_islandAnimation.Play();
        SoundsManager.PlaySound(SoundsManager.EcoConstructionSound);
    }

    void OnPlanetDeath() {
        isAlive = false;
        //mTODO Sergi: Maybe disable smoke effects
    }
}
