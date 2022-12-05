using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed;
    public float fireRate;
    public float maxHealth;
    public float health;
    public bool invincible;
    public GameObject bulletPrefab;

    private float canShoot;
    private Animator animator;
    private SoundManager soundManager;

    void Start() {
        speed = 5.0f;
        fireRate = 0.4f;
        maxHealth = 100.0f;
        health = maxHealth;
        invincible = false;

        canShoot = -1.0f;
        animator = GetComponent<Animator>();
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    void Update() {
        movementRoutine();

        combatRoutine();
    }

    public void hurt(float amount) {
        if (!invincible) {
            health -= amount;
            invincible = true;
            StartCoroutine(countInvincibility());
        }
    }

    private void movementRoutine() {
        Vector3 movementOffset = new Vector3(0, 0, 0);
        if (Input.GetKey("w")) {
            movementOffset += new Vector3(0, speed * Time.deltaTime, 0);
            goAnimationIdle();
            animator.SetBool("isRunningUp", true);
        }
        else if (Input.GetKey("s")) {
            movementOffset += new Vector3(0, -speed * Time.deltaTime, 0);
            goAnimationIdle();
            animator.SetBool("isRunningDown", true);
        }
        if (Input.GetKey("d")) {
            movementOffset += new Vector3(speed * Time.deltaTime, 0, 0);
            goAnimationIdle();
            animator.SetBool("isRunningRight", true);
        }  
        else if (Input.GetKey("a")) {
            movementOffset += new Vector3(-speed * Time.deltaTime, 0, 0);
            goAnimationIdle();
            animator.SetBool("isRunningLeft", true);
        }

        if (movementOffset == Vector3.zero)
            goAnimationIdle();

        transform.position += movementOffset;
    }

    private void combatRoutine() {
        if (Input.GetKey("up") && Time.time > canShoot) {
            canShoot = Time.time + fireRate;
            Vector3 bulletPosition = transform.position + new Vector3(0, 0.25f, 0);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, 90));
            bullet.GetComponent<Bullet>().flyDirection = new Vector3(0, 1, 0);
            soundManager.playSound(soundManager.bullet);
        }

        if (Input.GetKey("right") && Time.time > canShoot) {
            canShoot = Time.time + fireRate;
            Vector3 bulletPosition = transform.position + new Vector3(0.25f, 0, 0);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, 0));
            bullet.GetComponent<Bullet>().flyDirection = new Vector3(1, 0, 0);
            soundManager.playSound(soundManager.bullet);
        }

        if (Input.GetKey("down") && Time.time > canShoot) {
            canShoot = Time.time + fireRate;
            Vector3 bulletPosition = transform.position - new Vector3(0, 0.25f, 0);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, -90));
            bullet.GetComponent<Bullet>().flyDirection = new Vector3(0, -1, 0);
            soundManager.playSound(soundManager.bullet);
        }

        if (Input.GetKey("left") && Time.time > canShoot) {
            canShoot = Time.time + fireRate;
            Vector3 bulletPosition = transform.position - new Vector3(0.25f, 0, 0);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, 180));
            bullet.GetComponent<Bullet>().flyDirection = new Vector3(-1, 0, 0);
            soundManager.playSound(soundManager.bullet);
        }

        if (health <= 0)
            GameObject.Find("UI Canvas").GetComponent<GameUIManager>().callGameOver();
    }

    private void goAnimationIdle() {
        animator.SetBool("isRunningDown", false);
        animator.SetBool("isRunningRight", false);
        animator.SetBool("isRunningLeft", false);
        animator.SetBool("isRunningUp", false);
    }

    private IEnumerator countInvincibility() {
        yield return new WaitForSeconds(2.0f);
        invincible = false;
    }
}
