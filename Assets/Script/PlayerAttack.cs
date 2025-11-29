using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    public Player player;
    public GameObject anchor;
    public Camera cam;
    public SpriteRenderer[] sprites;

    private Vector3 mousePos;

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousePos - anchor.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        anchor.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        player.animator.SetInteger("Facing", rotation.x >= 0 ? 1 : -1);

        if (rotation.x != 0)
        {
            FlipSprite(rotation.x < 0);
        }
    }

    void FlipSprite(bool value)
    {
        foreach (SpriteRenderer s in sprites)
        {
            s.flipX = value;
        }
    }
}