using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] float m_detectionRange = 10.0f;
    [SerializeField] float m_speed = 4.0f;

    private float cooldownTimer = Mathf.Infinity;


    private Animator anim;
    private PlayerHealth playerHealth;

    private Transform playerTransform;
    private Rigidbody2D m_body2d;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        m_body2d = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Takeshi_Player"); // Finding the player by name
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player object 'Takeshi_Player' not found");
        }
    }

    private void Update(){
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?

        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= m_detectionRange)
        {
            Debug.Log("Player detected within range.");
            // Move towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            m_body2d.velocity = new Vector2(direction.x * m_speed, m_body2d.velocity.y);

            // Swap direction of sprite depending on walk direction
            if (direction.x > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (direction.x < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Run animation
            anim.SetBool("moving", true);

            if (PlayerInSight())
            {
                anim.SetBool("moving", false);
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("attack");
                }
            }
        }
        else
        {
            // Idle animation
            anim.SetBool("moving", false);
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
          playerHealth = hit.transform.GetComponent<PlayerHealth>();

        return hit.collider != null;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.Damage(damage);
        }
    }
}
