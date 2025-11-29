using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps; 

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float slowMultiplier = 0.5f;

    [Header("References")]
    public Tilemap slowTilemap; 
    Player player;
    Rigidbody2D rb;
    Animator animator;
    Statistics stats;

    Vector2 movement;
    float currentSpeed;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = player.stats;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        CalculateSpeed();

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * currentSpeed * Time.fixedDeltaTime);
    }

    void CalculateSpeed()
    {
        float speedMult = 1f;

        Vector3Int playerCell = slowTilemap.WorldToCell(transform.position);
        speedMult *= slowTilemap.HasTile(playerCell) ? slowMultiplier : 1f;

        currentSpeed = stats.baseMoveSpeed * speedMult;
    }
}