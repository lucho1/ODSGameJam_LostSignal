using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentDebtUpdater : MonoBehaviour
{
    Text m_text;

    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<Text>();
        int percent = Mathf.RoundToInt(GameOptions.IncreasedDebt * 100);
        m_text.text ="+" + percent.ToString() + "%";
    }
}
