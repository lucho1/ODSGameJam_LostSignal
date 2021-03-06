﻿using UnityEngine;
using System.Collections.Generic;

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
    [TagSelector]
    public string GroundTag;
    [TagSelector]
    public string DecorationTag;

    TypeOfCell type;
    bool isWater;
    bool isAlive = true;
    PlanetScript m_Planet;

    GameObject m_islandObject;
    Animation m_islandAnimation;
    GameObject m_treesObject;
    GameObject m_factoryObject;
    GameObject m_ecoObject;
    GameObject m_groundObject;
    List<GameObject> m_decorations;


    static bool firstFactory, firstEco, firstDestroy = false;

    GameManager.TypeOfConstruction Construction = GameManager.TypeOfConstruction.None;

    // Start is called before the first frame update
    void Start() {
        m_decorations = new List<GameObject>();
        m_Planet = GetComponentInParent<PlanetScript>();
        m_Planet.planetDeath.AddListener(OnPlanetDeath);
        m_Planet.planetContaminationChanged.AddListener(OnContaminationChanged);

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
            m_islandAnimation.Play();
            return;
        }

        switch (GameManager.SelectedConstruction) {
            default:
                SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
                m_islandAnimation.Play();
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
            else if (c.CompareTag(GroundTag))
                m_groundObject = c.gameObject;
            else if (c.CompareTag(DecorationTag))
                m_decorations.Add(c.gameObject);
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
            m_islandAnimation.Play();
            return;
        }

        int totalCost = Mathf.RoundToInt(GameOptions.DestructionCost * (1 + (GameOptions.FactoryCostIncrease * m_Planet.PlanetId)));
        if (GameManager.CurrentResources < totalCost) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            m_islandAnimation.Play();
            return;
        }

        if (!firstDestroy) {
            firstDestroy = true;
            DialogWindow.ActivateDialog(DialogWindow.Instance.FactoryDestroyed);
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
            m_islandAnimation.Play();
            return;
        }

        int totalCost = Mathf.RoundToInt(GameOptions.StandardFactoryCost * (1 + (GameOptions.FactoryCostIncrease * m_Planet.PlanetId)));
        if (GameManager.CurrentResources < totalCost) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            m_islandAnimation.Play();
            return;
        }

        if (!firstFactory) {
            firstFactory = true;
            DialogWindow.ActivateDialog(DialogWindow.Instance.FirstfactoryText);
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
            m_islandAnimation.Play();
            return;
        }

        int totalCost = Mathf.RoundToInt(GameOptions.EcoFactoryCost * (1 + (GameOptions.FactoryCostIncrease * m_Planet.PlanetId)));
        if (GameManager.CurrentResources < totalCost) {
            SoundsManager.PlaySound(SoundsManager.FailedConstructionSound);
            m_islandAnimation.Play();
            return;
        }

        if (!firstEco) {
            firstEco = true;
            DialogWindow.ActivateDialog(DialogWindow.Instance.FirstCleanFactorytext);
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
        m_treesObject.SetActive(false);
        //mTODO Sergi: Maybe disable smoke effects
        foreach (GameObject decoration in m_decorations)
            decoration.SetActive(false);
    }

    void OnContaminationChanged() {
        float myContamination = m_Planet.currentHealth == 0 ? 1 : 1 - (m_Planet.currentHealth/100);
        Material m_material = m_groundObject.GetComponent<Renderer>().material;
        if (m_material)
            m_material.SetFloat("_Contamination", myContamination);
    }
}
