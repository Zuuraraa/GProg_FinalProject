using System;
using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemSlot itemSlot;
    [Header("Audio Settings")]
    public AudioClip useSound;
    public virtual bool ValidToSwapInto()
    {
        return true;
    }

    public virtual bool ValidToSwapOut()
    {
        return true;
    }

    public abstract void CheckUse(AudioSource source);

    public abstract void HandleUse();

    public abstract void HandleSwapIn();

}
