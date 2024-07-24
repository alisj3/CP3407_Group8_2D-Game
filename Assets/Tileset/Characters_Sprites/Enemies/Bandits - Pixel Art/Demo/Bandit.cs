using UnityEngine;

public class BanditAI : MonoBehaviour {

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_attackRange = 3f;
    [SerializeField] float m_detectionRange = 10.0f;
    [SerializeField] float m_health = 100.0f; // Health of the bandit
    [SerializeField] float m_gravityScale = 1.0f; // Gravity scale for controlling jump height

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_isDead = false;

    private Transform playerTransform;

    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_body2d.gravityScale = m_gravityScale; // Set gravity scale
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        GameObject player = GameObject.Find("Takeshi_Player");
        
        if (player != null) {
            playerTransform = player.transform;
        } else {
            Debug.LogError("Player object 'Takeshi_Player' not found");
        }
    }
    
    void Update () {
        // Check if the bandit is dead
        if (m_health <= 0 && !m_isDead) {
            m_isDead = true;
            m_animator.SetTrigger("HeavyBandit_Death");
            m_body2d.velocity = Vector2.zero;
            return;
        }

        // Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Check if character just started falling
        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Check if the player is within detection range
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= m_detectionRange) {
            Debug.Log("Player detected within range.");
            // Move towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            m_body2d.velocity = new Vector2(direction.x * m_speed, m_body2d.velocity.y);

            // Swap direction of sprite depending on walk direction
            if (direction.x > 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (direction.x < 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Attack if within range
            if (Vector2.Distance(transform.position, playerTransform.position) <= m_attackRange) {
                Debug.Log("Player within attack range, attacking.");
                m_animator.SetTrigger("HeavyBandit_Attack");
            }

            // Run animation
            m_animator.SetInteger("AnimState", 2);
        } else {
            // Idle animation
            m_animator.SetInteger("AnimState", 0);
        }

        // Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);
    }

    // Method to apply damage to the bandit
    public void TakeDamage(float damage) {
        m_health -= damage;
        if (m_health > 0) {
            m_animator.SetTrigger("HeavyBandit_Hurt");
        }
        if (m_health <= 0 && !m_isDead) {
            m_isDead = true;
            m_animator.SetTrigger("HeavyBandit_Death");
            m_body2d.velocity = Vector2.zero;
        }
    }
}
