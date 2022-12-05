using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {
    public float velocity;
    public float damage;
    public float meleeRange;
    public float fireRate;
    public GameObject bulletPrefab;

    private Animator animator;
    private GameObject target;
    private SoundManager soundManager;
    private float canShoot;
    public void Start() {
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.Find("Player");
        canShoot = -1.0f;
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }
    public void Update() {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 targetDirection = target.transform.position - transform.position;
        if (distance < meleeRange) {
            target.GetComponent<Player>().hurt(damage);
            gameObject.GetComponent<Monster>().goAnimationIdle();
            animator.SetBool("isAttacking", true);
        }
        else if (Time.time > canShoot)
        {
            canShoot = Time.time + fireRate;

            for (float i = 0; i < Mathf.Deg2Rad * 360.0f; i += Mathf.Deg2Rad * 360.0f / 16.0f) {
                Quaternion rotation = Quaternion.Euler(0, i, 0);
                GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);

                Vector3 bulletDir = new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0.0f);
                bullet.GetComponent<MonsterBullet>().flyDirection = bulletDir;
            }

            soundManager.playSound(soundManager.bullet);
        }
        else {

            transform.position += targetDirection * velocity * Time.deltaTime;
            gameObject.GetComponent<Monster>().goAnimationIdle();
            animator.SetBool("isRunning", true);
        }
    }
}
