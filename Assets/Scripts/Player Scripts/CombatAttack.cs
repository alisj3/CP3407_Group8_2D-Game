using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAttack : MonoBehaviour
{

    public bool isAttacking = false;


    public Animator myAnim;
    public static CombatAttack instance;

    private void Awake()
    {

        myAnim = GetComponentInChildren<Animator>();
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isAttacking)
        {
            isAttacking = true;
        }
    }
}
