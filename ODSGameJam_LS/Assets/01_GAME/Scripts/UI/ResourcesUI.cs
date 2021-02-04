using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI: MonoBehaviour
{

    private int resources;

    public int DisplayNumber;

    public GameObject TMP;
    public bool changes = false;

    void Start()
    {
        resources = GameManager.CurrentResources;
        DisplayNumber = 0;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (changes)
        {
            PopupScore.CreatePopup(TMP, this.transform.position, DisplayNumber);
            
            changes = false;
        }
    }

}
