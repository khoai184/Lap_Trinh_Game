using UnityEngine;
using UnityEngine.InputSystem; 

public class Lab1_AudioControl : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            audioSource.Play();
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            audioSource.Stop();
        }
    }
}