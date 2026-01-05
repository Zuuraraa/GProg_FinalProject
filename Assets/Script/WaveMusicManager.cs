using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; 

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Music Clips")]
    public AudioClip calmMusic;   
    public AudioClip battleMusic; 

    private void Awake()
    {
    
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
    
        PlayCalmMusic();
    }


    public void PlayCalmMusic()
    {
    
        if (audioSource.clip != calmMusic)
        {
            audioSource.clip = calmMusic;
            audioSource.Play();
        }
    }

    
    public void PlayBattleMusic()
    {
    
        if (audioSource.clip != battleMusic)
        {
            audioSource.clip = battleMusic;
            audioSource.Play();
        }
    }
}