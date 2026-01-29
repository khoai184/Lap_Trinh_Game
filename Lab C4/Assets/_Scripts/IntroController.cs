using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public VideoPlayer introVideo;
    public Button skipButton;
    public string gameplaySceneName = "GameplayScene"; 

    void Start()
    {
        introVideo.loopPointReached += OnVideoFinished;

        if (skipButton != null)
        {
            skipButton.onClick.AddListener(LoadGameplay);
        }
    }

    void OnVideoFinished(VideoPlayer source)
    {
        LoadGameplay();
    }

    public void LoadGameplay()
    {
        Debug.Log("Đang chuyển vào Gameplay...");
        SceneManager.LoadScene(gameplaySceneName);
    }
}