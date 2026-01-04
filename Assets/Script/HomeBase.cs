using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HomeBase : Character
{
    [SerializeField] TilemapRenderer graphics;
    [SerializeField] HomeBaseTrackingGridController controller;
    
    BoxCollider2D boxCollider;
    protected override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public override void OnDeath()
    {
        graphics.enabled = false;
        boxCollider.enabled = false;
        gameObject.SetActive(false);
        controller.RecalculateFLowField();
    }
}
