using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps; 

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float slowMultiplier = 0.5f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip footstepSound;

    [Header("References")]
    public Player player;
    public Tilemap slowTilemap;

    Vector2 movement;
    float currentSpeed;

    private void Start()
    {
        Debug.Assert(slowTilemap != null, "Must add slowTilemap.");
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        CalculateSpeed();

        Animator animator = player.GetAnimator();
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetInteger("DirectionX", movement.x >= 0 ? 1 : -1);
    }

    void FixedUpdate()
    {
        Rigidbody2D rb = player.GetRB();
        rb.MovePosition(rb.position + currentSpeed * Time.fixedDeltaTime * movement.normalized);
    }

    void CalculateSpeed()
    {
        float speedMult = 1f;

        Vector3Int playerCell = slowTilemap.WorldToCell(transform.position);
        speedMult *= slowTilemap.HasTile(playerCell) ? slowMultiplier : 1f;

        currentSpeed = player.stats.baseMoveSpeed * speedMult;
    }

    public void PlayFootstep()
    {
        Debug.Log("Drap drap drap! (Fungsi terpanggil)");

        if (audioSource != null && footstepSound != null && movement.sqrMagnitude > 0.1f)
        {
        
            audioSource.pitch = Random.Range(0.8f, 1.2f);
    
            audioSource.PlayOneShot(footstepSound);
        }
    }
}