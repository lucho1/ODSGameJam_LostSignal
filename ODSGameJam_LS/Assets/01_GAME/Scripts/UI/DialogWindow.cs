using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : Singleton<DialogWindow>
{
    public TMPro.TextMeshProUGUI dialog;
    public float disappearTime = 5.0f;
    public float vanishRate = 0.2f;
    private float dialogTimer;
    public GameObject nameGO;
    public GameObject dialogGO;
    public GameObject imageGO;
    public GameObject closeButton;


    [HideInInspector] public string GameStartText = "We need some energy! Start builing factories.";
    [HideInInspector] public string GameStartText2 = "If you run out of space in the planet, try colonizing other planets to build there. Make Valentina Tereshkova proud!";
    [HideInInspector]public string FirstfactoryText = "Wow! Look at those factories! I hope those chimneys have some filters though.Which by the way, were invented by Mary Walton in 1870. That's it, some free knowledge for you.";
    [HideInInspector]public string ManyFactoriesText = "Be careful with those factories, they polute a lot. Wangari Maathai wouldn't like it. She won a nobel prize, you probably don't know her...";
    [HideInInspector]public string AnotherOneText = "And another one!";
    [HideInInspector]public string FirstCleanFactorytext = "Hey, that factory does not polute at all! Nice!";
    [HideInInspector]public string CleanFactoryText = "This is the legacy of Hertha Marks!";
    [HideInInspector]public string CleanFactoryText2 = "Give me that green energy baby!";
    [HideInInspector]public string FactoryDestroyed = "Well, there it goes a lot of energy income, but reducing the polution is more important. Greta Thumberg would be proud for sure.";
    [HideInInspector]public string FactoryDestroyed2 = "Down it goes!";
    [HideInInspector] public string FactoryDestroyed3 = "We might run out of energy but I have to admit, it is really fun to see things go down.";
    
    // Start is called before the first frame update
    void Start()
    {
        dialog.text = GameStartText2;
        dialogTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - dialogTimer > disappearTime)
            DeactivateDialog();
    }

    public static void ActivateDialog(string text)
    {
        Instance.dialog.text = text;
        //Instance.nameGO.SetActive(true);
        Instance.dialogGO.SetActive(true);
        Instance.imageGO.SetActive(true);
        Instance.closeButton.SetActive(true);
        Instance.dialogTimer = Time.time;
    }

    public static void DeactivateDialog()
    {
        //Instance.nameGO.SetActive(false);
        Instance.dialogGO.SetActive(false);
        Instance.imageGO.SetActive(false);
        Instance.closeButton.SetActive(false);
    }
}
