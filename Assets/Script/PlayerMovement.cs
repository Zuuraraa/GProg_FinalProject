using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps; 

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float slowMultiplier = 0.5f;

    [Header("References")]
    public Player player;
    public Tilemap slowTilemap;

    Vector2 movement;
    float currentSpeed;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        CalculateSpeed();

        player.animator.SetFloat("Speed", movement.sqrMagnitude);
        player.animator.SetInteger("DirectionX", movement.x >= 0 ? 1 : -1);
    }

    void FixedUpdate()
    {
        player.rb.MovePosition(player.rb.position + currentSpeed * Time.fixedDeltaTime * movement.normalized);
    }

    void CalculateSpeed()
    {
        float speedMult = 1f;

        Vector3Int playerCell = slowTilemap.WorldToCell(transform.position);
        speedMult *= slowTilemap.HasTile(playerCell) ? slowMultiplier : 1f;

        currentSpeed = player.stats.baseMoveSpeed * speedMult;
    }

   
}