using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HomeBase : Character
{
    public static List<HomeBase> homeBaseList = new List<HomeBase>();
    [SerializeField] TilemapRenderer graphics;
    [SerializeField] HomeBaseTrackingGridController controller;

    [Header("Audio Settings")]
    public AudioSource audioSource; 
    public AudioClip hitSound;      
    public AudioClip destroySound;
    
    BoxCollider2D boxCollider;
    protected override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider2D>();
        homeBaseList.Add(this);
    }
    protected override void OnDamage(string originCode = "")
    {
        base.OnDamage(originCode);

        if (audioSource != null && hitSound != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(hitSound);
        }
    }

    public override void OnDeath()
    {
        if (destroySound != null)
        {
            AudioSource.PlayClipAtPoint(destroySound, transform.position);
        }

        graphics.enabled = false;
        boxCollider.enabled = false;
        gameObject.SetActive(false);
        controller.RecalculateFLowField();
    }

    public static void HealSomeHP()
    {
        foreach (HomeBase homeBase in homeBaseList)
        {
            homeBase.TakeDamage(-(int)((float)(homeBase.stats.maxHP) * 0.02));
        }
    }
}
