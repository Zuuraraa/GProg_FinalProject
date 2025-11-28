using UnityEngine;
using UnityEngine.Tilemaps; 

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float slowMultiplier = 0.5f; 

    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public Tilemap slowTilemap; 

    Vector2 movement;
    float currentSpeed; 

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector3Int playerCell = slowTilemap.WorldToCell(transform.position);

        if (slowTilemap.HasTile(playerCell))
        {
            currentSpeed = moveSpeed * slowMultiplier;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

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
}