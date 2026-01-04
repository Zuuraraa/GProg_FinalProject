using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
    public float defaultSpeed = 5f;


    CircleCollider2D detectionCollider;
    CapsuleCollider2D collisionCollider;

    Transform target = null;
    float speed = 5f;

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
            speed += Time.deltaTime * 2;
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
            CollisionResult(player);
            gameObject.SetActive(false);
        }
    }

    protected abstract void CollisionResult(Player player);

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public virtual void Reset()
    {
        speed = defaultSpeed;
        target = null;
    }
}
