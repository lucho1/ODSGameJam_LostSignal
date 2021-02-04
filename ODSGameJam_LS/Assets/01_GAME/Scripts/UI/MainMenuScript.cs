using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuObject;

    [SerializeField]
    private GameObject CreditsObject;

    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }

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

    public void QuitButton()
    {
        Application.Quit();
    }
}
