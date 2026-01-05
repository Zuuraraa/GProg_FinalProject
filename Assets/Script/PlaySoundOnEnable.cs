using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    [Header("Settings")]
    public AudioSource audioSource; 
    public AudioClip soundClip;     

    private void OnEnable()
    {
        if (audioSource != null && soundClip != null)
        {
            audioSource.PlayOneShot(soundClip);
        }
    }
}