using UnityEngine;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    [SerializeField]
    private Text TitleText, VictoryText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameResults.IsVictory)
        {
            TitleText.text = "Victory!";
            VictoryText.text = "You managed to save the planet!\nYou needed to colonize " + GameResults.TotalPlanets +
                                " planets to accomplish it, and you killed " + GameResults.DeadPlanets + " in your way."
                                    + "\n\nYou also got the whole needed resources to save The Earth and gathered additional "
                                        + GameResults.FinalResources + " resources.";
        }
        else
        {
            TitleText.text = "Game Over";
            VictoryText.text = "Oh no! Our planet was destroyed by polution\nYou needed to colonize " + GameResults.TotalPlanets +
                                " planets and kill " + GameResults.DeadPlanets + " other planets in your way to save The Earth and yet you didn't accomplish it"
                                    + "\n\nYou finally stored " + GameResults.FinalResources + " resources.";
        }
    }
}
