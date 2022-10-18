using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public float speed;
    private bool movingLeft = true;
    public Transform groundDetection;
    public float rayLength;

    public int maxhealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0){
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayLength);
            if (groundInfo.collider == false) {
                if (movingLeft == true) {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingLeft = false;
                } else {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingLeft = true;
                }
            }
            animator.SetFloat("Speed", speed);
        }
    }

    public void TakeDamage (int damage) {
        currentHealth -= damage;
        //Play damaged animation
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Enemy Died");
        //Die animation
        animator.SetBool("IsDead", true);
        //Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
