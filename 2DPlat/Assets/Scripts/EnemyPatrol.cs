using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private bool movingLeft = true;
    public Transform groundDetection;
    public float rayLength;
    public Animator animator;

    void movement() {
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
