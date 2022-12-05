using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour {
    public float attack;
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
        if (collision.gameObject.tag != "Monster") {
            animator.SetBool("isExploding", true);

            if (collision.gameObject.tag == "Player")
                collision.gameObject.GetComponent<Player>().hurt(attack);
        }
    }

    void Explode() {
        Destroy(gameObject);
    }
}
