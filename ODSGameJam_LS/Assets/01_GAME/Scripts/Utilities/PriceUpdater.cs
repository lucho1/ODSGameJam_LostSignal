using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceUpdater : MonoBehaviour
{
    public enum Type {
        Factory,
        EcoFactory,
        Destroy,
        Colonize
    }

    public Type PriceType = Type.Factory;
    Text m_PriceText;

    void Start() {
        m_PriceText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int cost = 0;
        switch (PriceType) {
            case Type.Factory:
                cost = Mathf.RoundToInt(GameOptions.StandardFactoryCost * (1 + (GameOptions.FactoryCostIncrease * GameManager.CurrentPlanetIndex)));
                break;
            case Type.EcoFactory:
                cost = Mathf.RoundToInt(GameOptions.EcoFactoryCost * (1 + (GameOptions.FactoryCostIncrease * GameManager.CurrentPlanetIndex)));
                break;
            case Type.Destroy:
                cost = Mathf.RoundToInt(GameOptions.DestructionCost * (1 + (GameOptions.FactoryCostIncrease * GameManager.CurrentPlanetIndex)));
                break;
            case Type.Colonize:
                cost = GameOptions.PlanetCost;
                break;
        }

        m_PriceText.text = cost.ToString();
    }
}
