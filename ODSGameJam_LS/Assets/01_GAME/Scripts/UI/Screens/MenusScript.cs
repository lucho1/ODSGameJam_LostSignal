using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusScript : MonoBehaviour
{
    [SerializeField]
    AudioClip HoverSound, ClickSound;

    private AudioSource m_AudioSource;

    [SerializeField]
    private GameObject MenuObject;

    [SerializeField]
    private GameObject CreditsObject;

    [SerializeField]
    private GameObject InstructionsObject;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // --- Generic ---
    public void PlayHoverSound()
    {
        //m_AudioSource.Stop();
        m_AudioSource.clip = HoverSound;
        m_AudioSource.Play();
    }

    private void PlayClickSound()
    {
        m_AudioSource.Stop();
        m_AudioSource.clip = ClickSound;
        m_AudioSource.Play();
    }

    public void CreditsButton()
    {
        PlayClickSound();
        MenuObject.SetActive(false);
        CreditsObject.SetActive(true);
    }

    public void CreditsReturnButton()
    {
        PlayClickSound();
        CreditsObject.SetActive(false);
        MenuObject.SetActive(true);
    }

    // --- Game Over Screen ---
    public void MainMenuButton()
    {
        PlayClickSound();
        SceneManager.LoadScene("MainMenu");
    }

    // --- Main Menu Screen ---
    public void PlayButton()
    {
        PlayClickSound();
        MenuObject.SetActive(false);
        InstructionsObject.SetActive(true);
    }    

    public void InstructionsReturnButton()
    {
        PlayClickSound();
        InstructionsObject.SetActive(false);
        MenuObject.SetActive(true);
    }

    public void InstructionsPlayButton()
    {
        PlayClickSound();
        SceneManager.LoadScene("GameScene");
    }

    public void QuitButton()
    {
        PlayClickSound();
        Application.Quit();
    }
}
