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
        Vector3 pos = transform.position;
        pos.y = transform.localPosition.y;
        PopupScore.CreatePopup(TMP, pos, DisplayNumber);
    }

}
