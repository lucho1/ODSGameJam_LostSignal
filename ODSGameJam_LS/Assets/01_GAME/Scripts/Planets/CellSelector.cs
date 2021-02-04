using UnityEngine;

public class CellSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseEnter() {
        // myTODO: Trigger outline here
        GameManager.CurrentSelectedCell = this.gameObject;
    }

    void OnMouseExit() {
        // myTODO: Untrigger outline here
        if (GameManager.CurrentSelectedCell == this.gameObject)
            GameManager.CurrentSelectedCell = null;
    }
}
