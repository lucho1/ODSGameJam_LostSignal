using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupScore2D : MonoBehaviour
{

    public float DisappearTime  = 1.0f;
    public float MoveSpeedY     = 2.0f;
    public float DisappearSpeed = 3.0f;

    [SerializeField]
    private Text DisplayText;

    RectTransform m_RectTransform;

    private Text m_DisplayText;

    void Start()
    {
        m_DisplayText = DisplayText.GetComponent<Text>();
        m_RectTransform = GetComponent<RectTransform>();      
    }


    // Update is called once per frame
    void Update()
    {
        m_RectTransform.localPosition += Vector3.up * Time.deltaTime* MoveSpeedY;

    }

   /* public static PopupScore2D CreatePopupScore2D(GameObject prefab, Vector3 position, int number)
    {
        GameObject newInstance = Instantiate(prefab, position,Quaternion.identity);

        if(newInstance == null)
        {
            return null;
        }

        PopupScore2D MyPop = newInstance.GetComponent<PopupScore2D>();
        MyPop.DisplayText = number.ToString();
    }
   */

}
