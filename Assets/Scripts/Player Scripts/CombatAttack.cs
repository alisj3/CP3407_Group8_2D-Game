using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CombatAttack : MonoBehaviour
{
    private GameObject AttackArea = default;
    public bool isAttacking = false;

    private float timer = 0f;
    private float attackTime = 0.25f;

    public Animator myAnim;
    public static CombatAttack instance;


    public bool attacking = false;

    private void Awake()
    {
        myAnim = GetComponentInChildren<Animator>();
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AttackArea = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= attackTime)
            {
                timer = 0;
                attacking = false;
                AttackArea.SetActive(attacking);
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        attacking = true;
        AttackArea.SetActive(attacking);
    }
}
