using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;  // how close to stop from player

    [Header("Attack")]
    public int contactDamage = 10;
    public float damageCooldown = 1f;  // seconds between hits

    Transform player;
    Rigidbody rb;
    float lastDamageTime = -999f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Find the player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("EnemyAI: No object with tag 'Player' found in the scene.");
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Direction from enemy to player (ignore height difference)
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            // Move toward player
            direction.Normalize();
            Vector3 velocity = direction * moveSpeed;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

            // Face the player
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else
        {
            // Close enough – stop moving (but keep gravity)
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (player == null) return;

        // Are we colliding with the player?
        if (collision.gameObject.CompareTag("Player"))
        {
            // Only damage every damageCooldown seconds
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                lastDamageTime = Time.time;

                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(contactDamage);
                }
            }
        }
    }
}
