using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoScript : MonoBehaviour
{
    VideoPlayer video;
    private float countTimer;

    // Start is called before the first frame update
    void Start()
    {
        video = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countTimer >= video.length + 0.5f)
            SceneManager.LoadScene("MainMenu");

        countTimer += Time.deltaTime;
    }
}
