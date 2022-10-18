using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public int attackDamage;
    public float attackRate = 1f;
    float nextAttackTime = 0f;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime) {
            if (Input.GetButtonDown("Fire1")) {
                        Attack();
                        nextAttackTime = Time.time + (1f / attackRate);
                }
        }
    }

    void Attack() {
        //Play attack animation
        animator.SetTrigger("Attack1");
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage enemies in range
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

        }
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
