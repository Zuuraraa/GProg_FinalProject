using System;
using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public virtual bool ValidToSwapInto()
    {
        return true;
    }

    public virtual bool ValidToSwapOut()
    {
        return true;
    }

    public abstract void CheckUse();

    public abstract void HandleUse();

    public abstract void HandleSwapIn();

}
