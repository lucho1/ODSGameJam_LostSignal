using UnityEngine;
using UnityEngine.EventSystems;

public class ConstructionSelector : MonoBehaviour, ISelectHandler
{
    public GameManager.TypeOfConstruction myType;

    public void OnSelect(BaseEventData eventData) {
        GameManager.SelectedConstruction = myType;
    }
}
