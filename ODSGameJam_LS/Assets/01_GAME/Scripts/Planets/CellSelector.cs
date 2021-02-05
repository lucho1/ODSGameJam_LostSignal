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
    PlanetScript m_Planet;

    GameObject m_islandObject;
    GameObject m_treesObject;
    GameObject m_factoryObject;
    GameObject m_ecoObject;
    // Start is called before the first frame update
    void Start() {
        m_Planet = GetComponentInParent<PlanetScript>();
        type = (TypeOfCell)Random.Range(0,3);

        if (m_Planet && m_Planet.GroundSpawned < m_Planet.MaxGround)
        {
            float Throw = Random.Range(0.0f,100.0f);
            isWater = Throw > 50.0f;

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

    void SetupPrefab() {
        if (!m_islandObject)
            return;

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
}
