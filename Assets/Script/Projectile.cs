using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    public IEnumerator Travel(Vector3 direction, float speed, float lifespan)
    {
        Vector3 startPos = transform.position;

        //destination += startPos;
        while (lifespan > 0)
        {
            lifespan -= Time.deltaTime;
            Debug.Log(lifespan);
            transform.position += direction * speed * Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tag"))
        {
            //Player player = collision.gameObject.GetComponent<Player>(); 
            
        }
        gameObject.SetActive(false);
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
}
