using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 25f;
    public float lifeTime = 2f;
    public int damage = 25;

    void Start()
    {
        // bullet disappears after some time so it doesn’t fill the scene
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // move forward continuously
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Only damage objects that have EnemyHealth script
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
