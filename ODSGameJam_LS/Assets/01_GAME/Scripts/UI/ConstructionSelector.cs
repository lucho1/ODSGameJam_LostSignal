using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConstructionSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameManager.TypeOfConstruction myType;
    public Color selectedColor;
    public Color highlightColor;
    public Color unselectedColor;

    private Color m_currentColor;
    
    Image m_image;
    bool m_isSelected = false;

    public void Start() {
        m_image = gameObject.GetComponent<Image>();
        m_image.color = unselectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (m_isSelected)
            return;
        m_currentColor = m_image.color;
        m_image.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (m_isSelected)
            return;
        m_image.color = m_currentColor;
    }

    public void SelectButton() {
        if (m_isSelected)
            return;

        GameManager.SelectedConstruction = myType;
        m_image.color = selectedColor;
        m_currentColor = selectedColor;
        m_isSelected = true;
    }

    public void DeselectButton() {
        if (!m_isSelected)
            return;
            
        m_image.color = unselectedColor;
        m_currentColor = unselectedColor;
        m_isSelected = false;
    }
}
