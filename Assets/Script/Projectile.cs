using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    public IEnumerator Travel(Vector3 destination)
    {
        Vector3 startPos = transform.position;
        float percentComplete = 0f;

        //destination += startPos;
        while (percentComplete < 1f)
        {
            percentComplete += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(startPos, destination, percentComplete);
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
