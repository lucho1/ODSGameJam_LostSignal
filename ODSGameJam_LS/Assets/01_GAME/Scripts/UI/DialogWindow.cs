using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dialog;
    public float disappearTime = 5.0f;
    public float vanishRate = 0.2f;
    private float dialogTimer;
    public GameObject nameGO;
    public GameObject dialogGO;
    public GameObject imageGO;
    public GameObject closeButton;



    private string GameStartText = "We need some energy! Start builing factories.";
    private string GameStartText2 = "If you run out of space in the planet, try colonizing other planets to build there. Make Valentina Tereshkova proud!";
    private string FirstfactoryText = "Wow! Look at those factories! I hope those chimneys have some filters though.Which by the way, were invented by Mary Walton in 1870. That's it, some free knowledge for you.";
    private string ManyFactoriesText = "Be careful with those factories, they polute a lot. Wangari Maathai wouldn't like it. She won a nobel prize, you probably don't know her...";
    private string AnotherOneText = "And aonther one!";
    private string FirstCleanFactorytext = "Hey, that factory does not polute at all! Nice!";
    private string CleanFactoryText = "This is the legacy of Hertha Marks!";
    private string CleanFactoryText2 = "Give me that green energy baby!";
    private string FactoryDestroyed = "Well, there it goes a lot of energy income, but reducing the polution is more important. Greta Thumberg would be proud for sure.";
    private string FactoryDestroyed2 = "Down it goes!"; 
    private string FactoryDestroyed3 = "We might run out of energy but I have to admit, it is really fun to see things go down.";

    // Start is called before the first frame update
    void Start()
    {
        dialog.text = GameStartText2;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ActivateDialog()
    {
        nameGO.SetActive(true);
        dialogGO.SetActive(true);
        imageGO.SetActive(true);
        closeButton.SetActive(true);
        dialogTimer = Time.time;
    }
}
