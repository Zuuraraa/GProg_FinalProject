using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [Header("Alat Pemutar")]
    public AudioSource sfxSource; 

    [Header("Kaset Suara")]
    public AudioClip clickClip;  

    public void PlayClickSound()
    {
        sfxSource.PlayOneShot(clickClip);
    }
}
