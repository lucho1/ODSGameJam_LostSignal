using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionSelector : MonoBehaviour
{
    public Button FactoryButton;
    public Button EcoFactoryButton;
    public Button DestroyButton;

    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.SelectedConstruction) {
            default:
            case GameManager.TypeOfConstruction.Factory:
                FactoryButton.Select();
            break;

        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
