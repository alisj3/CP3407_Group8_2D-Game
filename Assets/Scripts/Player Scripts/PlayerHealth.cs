using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] Animator DieAnimation;

    private int MAX_HEALTH = 100;

    private void Awake()
    {
        DieAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;
        DieAnimation.SetTrigger("hurt");

        if (health < 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Heal");
        }

        if (health + amount > MAX_HEALTH)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }

    }

    private void Die()
    {
        Debug.Log("You Died");
        GetComponentInParent<MeleeAttack>().enabled = false;
        GetComponentInParent<MeleeAttack>().m_body2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        DieAnimation.SetTrigger("die");

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
