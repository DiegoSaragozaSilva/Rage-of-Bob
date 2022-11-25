using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public float fireRate;
    public GameObject bulletPrefab;

    private float canShoot;
    private Animator animator;

    void Start() {
        speed = 5.0f;
        fireRate = 0.4f;

        canShoot = -1.0f;
        animator = GetComponent<Animator>();
    }

    void Update() {
        movementRoutine();

        combatRoutine();
    }

    private void movementRoutine() {
        Vector3 movementOffset = new Vector3(0, 0, 0);
        if (Input.GetKey("w"))
            movementOffset += new Vector3(0, speed * Time.deltaTime, 0);
        else if (Input.GetKey("s"))
            movementOffset += new Vector3(0, -speed * Time.deltaTime, 0);  
        if (Input.GetKey("d"))
            movementOffset += new Vector3(speed * Time.deltaTime, 0, 0);  
        else if (Input.GetKey("a"))
            movementOffset += new Vector3(-speed * Time.deltaTime, 0, 0);

        if (movementOffset != Vector3.zero)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

        transform.position += movementOffset;
    }

    private void combatRoutine() {
        if (Input.GetKey("up") && Time.time > canShoot) {
            canShoot = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 90));
            bullet.GetComponent<Bullet>().flyDirection = new Vector3(0, 1, 0);
        }

        if (Input.GetKey("right") && Time.time > canShoot) {
            canShoot = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            bullet.GetComponent<Bullet>().flyDirection = new Vector3(1, 0, 0);
        }

        if (Input.GetKey("down") && Time.time > canShoot) {
            canShoot = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, -90));
            bullet.GetComponent<Bullet>().flyDirection = new Vector3(0, -1, 0);
        }

        if (Input.GetKey("left") && Time.time > canShoot) {
            canShoot = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 180));
            bullet.GetComponent<Bullet>().flyDirection = new Vector3(-1, 0, 0);
        }
    }
}
