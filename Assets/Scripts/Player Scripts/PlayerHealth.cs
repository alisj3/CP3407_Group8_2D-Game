using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] Animator DieAnimation;

    private int MAX_HEALTH = 10;
    public GameObject Blood;
    public AudioSource audioSource;
    public AudioClip dieSound;

    private void Awake()
    {
        DieAnimation = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    [SerializeField] private UnityEngine.UI.Image _healthBarForegroundImage;

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
        Instantiate(Blood, transform.position, Quaternion.identity);
        audioSource.Play();


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
        if (gameObject.CompareTag("Enemy"))
        {
            GetComponentInParent<MeleeAttack>().enabled = false;
            GetComponentInParent<MeleeAttack>().m_body2d.constraints = RigidbodyConstraints2D.FreezePositionX;
            DieAnimation.SetTrigger("die");

            StartCoroutine(Delay());
        }
        else if (gameObject.CompareTag("Player"))
        {
            audioSource.clip = dieSound;
            audioSource.Play();

            GetComponentInParent<PlayerMovement>().enabled = false;
            GetComponentInParent<CharacterController2D>().enabled = false;
            GetComponentInParent<CombatAttack>().enabled = false;

            DieAnimation.SetTrigger("die");
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
