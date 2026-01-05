using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStatistics stats = (EnemyStatistics) enemy.stats;

        if (collision.CompareTag("Player") || collision.CompareTag("HomeBase"))
        {
            Character character = collision.tag == "Player" ? collision.GetComponent<Player>() : collision.GetComponent<HomeBase>();
            character.TakeDamage(stats.damage);
            StartCoroutine(AttackDelay());
        }
    }

    protected IEnumerator AttackDelay()
    {
        circleCollider.enabled = false;
        enemy.graphics.color = new Color(.75f, .75f, .75f);
        yield return new WaitForSeconds(.5f);
        circleCollider.enabled = true;
        enemy.graphics.color = new Color(1, 1, 1);
        yield break;
    }
}
