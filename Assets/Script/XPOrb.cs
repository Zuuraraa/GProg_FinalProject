using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class XPOrb : MonoBehaviour
{
    [SerializeField] float speed = 5;
    public int value = 1;


    CircleCollider2D detectionCollider;
    CapsuleCollider2D collisionCollider;
    Transform target = null;

    private void Awake()
    {
        detectionCollider = gameObject.GetComponent<CircleCollider2D>();
        collisionCollider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (target)
        {
            transform.position += speed * Time.deltaTime * (target.position - transform.position).normalized;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetTarget(collision.transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.GainXp(value);
            gameObject.SetActive(false);
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
