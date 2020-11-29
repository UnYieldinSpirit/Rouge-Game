using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public Collider2D playerCollider;
    public Animator anim;
    public Transform attackPoint;
    public ScoreScript score;
    public LayerMask playerLayer;
    public float atkRange;
    public int maxHealth = 100;
    public int atkdamage = 1;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        attackPoint = GetComponent<Transform>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        //Play hurt animation
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        OnTriggerEnter2D(playerCollider);
        Attack();
    }



    void Die()
    {
        Debug.Log("Enemy Died");
        anim.SetBool("IsDead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        score.UpdateScore(50);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject == player)
        {
            player.TakeDamage(atkdamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, atkRange);
    }

        void Attack()
    {
        //Detect enemies in range of an attack
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, atkRange, playerLayer);
        //Apply damage to enemies in range
        foreach(Collider2D play in hitPlayer)
        {
            Debug.Log("Hit Player");
            play.GetComponent<Player>().TakeDamage(atkdamage);
        }
    }
}
