using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI: MonoBehaviour
{

    private int resources;

    public GameObject TMP;
   

    void Start()
    {
        resources = GameManager.CurrentResources;
        
    }

    

    // Update is called once per frame
    void Update()
    {
     
    }

    public void PopChanges(int DisplayNumber)
    {
        PopupScore.CreatePopup(TMP, transform.localPosition, DisplayNumber);
    }

}
