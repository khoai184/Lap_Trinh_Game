using UnityEngine;
using UnityEngine.Video;

public class Lab7_VideoEvents : MonoBehaviour
{
    private VideoPlayer vp;
    public GameObject endUI;
    void Start()
    {
        vp = GetComponent<VideoPlayer>();

        vp.prepareCompleted += OnVideoPrepared;

        vp.loopPointReached += OnVideoEnd;

        vp.Prepare();
    }

    void OnVideoPrepared(VideoPlayer source)
    {
        Debug.Log("Video đã load xong và sẵn sàng!");
        source.Play();
    }

    void OnVideoEnd(VideoPlayer source)
    {
        Debug.Log("Video đã chạy hết rồi!");

        if (endUI != null)
        {
            endUI.SetActive(true);
        }
    }
}