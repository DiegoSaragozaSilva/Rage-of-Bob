using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour {
    public float velocity;
    public float meleeRange;
    public float damage;

    private Animator animator;
    private GameObject target;
    public void Start() {
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.Find("Player");
    }
    public void Update() {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance < meleeRange) {
            target.GetComponent<Player>().hurt(damage);
            gameObject.GetComponent<Monster>().goAnimationIdle();
            animator.SetBool("isAttacking", true);
        }
        else {
            Vector3 targetDirection = target.transform.position - transform.position;
            transform.position += targetDirection * velocity * Time.deltaTime;
            gameObject.GetComponent<Monster>().goAnimationIdle();
            animator.SetBool("isRunning", true);
        }
    }
}
