using System.Collections;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rb;
    public Animator animator;
    private void Awake()
    {
        stats = (PlayerStatistics) ScriptableObject.CreateInstance(nameof(PlayerStatistics));
    }




}
