using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : MonoBehaviour {
    public float velocity;
    public float damage;
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
        Vector3 targetDirection = target.transform.position - transform.position;
        transform.position += targetDirection * velocity * Time.deltaTime;
        gameObject.GetComponent<Monster>().goAnimationIdle();
        animator.SetBool("isRunning", true);

        if (Time.time > canShoot) {
            canShoot = Time.time + fireRate;
            float bulletDirection = Vector3.Angle(transform.position, target.transform.position);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, bulletDirection));

            targetDirection.Normalize();
            bullet.GetComponent<MonsterBullet>().flyDirection = targetDirection;
            soundManager.playSound(soundManager.bullet);
        }
    }
}
