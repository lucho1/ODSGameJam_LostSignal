using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusScript : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuObject;

    [SerializeField]
    private GameObject CreditsObject;

    [SerializeField]
    private GameObject InstructionsObject;

    // --- Generic ---
    public void CreditsButton()
    {
        MenuObject.SetActive(false);
        CreditsObject.SetActive(true);
    }

    public void CreditsReturnButton()
    {
        CreditsObject.SetActive(false);
        MenuObject.SetActive(true);
    }

    // --- Game Over Screen ---
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // --- Main Menu Screen ---
    public void PlayButton()
    {
        MenuObject.SetActive(false);
        InstructionsObject.SetActive(true);
    }    

    public void InstructionsReturnButton()
    {
        InstructionsObject.SetActive(false);
        MenuObject.SetActive(true);
    }

    public void InstructionsPlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
