using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float statusBarOffset;
    public bool isBoss;
    public GameObject monsterStatusUIPrefab;

    private Animator animator;
    private GameObject monsterStatusUIObject;

    public void Start() {
        health = maxHealth;

        animator = gameObject.GetComponent<Animator>();

        monsterStatusUIObject = Instantiate(monsterStatusUIPrefab, GameObject.Find("UI Canvas").transform);
        monsterStatusUIObject.transform.GetChild(0).gameObject.GetComponent<MonsterHealthbar>().monster = gameObject;
        monsterStatusUIObject.transform.GetChild(0).gameObject.GetComponent<MonsterHealthbar>().headOffset = statusBarOffset;

        RoomManager roomManager = GameObject.Find("Room Manager").GetComponent<RoomManager>();
        roomManager.notifyMonsterSpawn();
    }

    public void Update() {
        if (health <= 0)
            kill();
    }

    public void reduceHealth(float amount) {
        health -= amount;
        animator.SetBool("isDamage", true);
    }

    private void kill() {
        Destroy(monsterStatusUIObject);
        Destroy(gameObject);

        RoomManager roomManager = GameObject.Find("Room Manager").GetComponent<RoomManager>();
        roomManager.notifyMonsterKill();

        if (isBoss)
            GameObject.Find("UI Canvas").GetComponent<GameUIManager>().callWin();
    }

        public void goAnimationIdle() {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDamage", false);
    }
}
