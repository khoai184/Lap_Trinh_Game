using UnityEngine;
using UnityEngine.Video; 
using UnityEngine.InputSystem; 

public class Lab5_VideoManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
                Debug.Log("Đang phát Video...");
            }
            else
            {
                videoPlayer.Pause(); 
                Debug.Log("Đã tạm dừng Video.");
            }
        }
    }
}