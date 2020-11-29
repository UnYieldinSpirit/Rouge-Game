using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage = 20;
    public LayerMask enemyLayer;
    void Start()
    {
        anim = GetComponent<Animator>();
        attackPoint = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();    
        }       
    }

    void Attack()
    {
        //Play an attack animation
        anim.SetTrigger("Attack");
        //Detect enemies in range of an attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        //Apply damage to enemies in range
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit");
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
