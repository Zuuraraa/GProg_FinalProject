using System.Collections;
using UnityEngine;

public class Chili : PlantAction
{
    [SerializeField] int activeFrames = 5;

    [Header("Reference")]
    [SerializeField] Collider2D hurtbox;
    [SerializeField] SpriteRenderer sprite;

    public override void DoAction()
    {
        StartCoroutine(ActionProccess());
    }

    IEnumerator ActionProccess()
    {
        hurtbox.enabled = true;
        sprite.enabled = true;
        yield return new WaitForSecondsRealtime(GameManager.FramesToSeconds(activeFrames));
        hurtbox.enabled = false;
        sprite.enabled = false;
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(plant.plantData.damage);
        }
    }
}
