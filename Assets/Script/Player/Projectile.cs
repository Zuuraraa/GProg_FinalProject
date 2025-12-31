using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public string originCode = "";

    public IEnumerator Travel(Vector3 direction, float speed, float lifespan)
    {
        Vector3 startPos = transform.position;

        //destination += startPos;
        while (lifespan > 0)
        {
            lifespan -= Time.deltaTime;
            transform.position += direction * speed * Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage, originCode);
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
}
