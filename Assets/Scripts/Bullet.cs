using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Vector3 flyDirection;
    public float speed;

    private Animator animator;

    void Start() {
        speed = 6.0f;

        animator = GetComponent<Animator>();
    }

    void Update() {
        transform.position += flyDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag != "Player")
            animator.SetBool("isExploding", true);
    }

    void Explode()
    {
        Destroy(gameObject);
    }
}
