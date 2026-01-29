using UnityEngine;
using UnityEngine.InputSystem;

public class Lab3_GlobalControl : MonoBehaviour
{
    private bool isMuted = false;
    private bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            isMuted = !isMuted;
            AudioListener.volume = isMuted ? 0f : 1f;
            Debug.Log(isMuted ? "Đã Mute âm thanh toàn cục" : "Đã Bật lại âm thanh");
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            isPaused = !isPaused;
            AudioListener.pause = isPaused;
            Debug.Log(isPaused ? "Đã Tạm dừng âm thanh" : "Đã Tiếp tục âm thanh");
        }
    }
}