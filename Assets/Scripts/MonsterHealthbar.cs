using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthbar : MonoBehaviour {
    public GameObject monster;
    public float headOffset;

    private Slider slider;

    public void Start() {
        slider = gameObject.GetComponent<Slider>();
    }

    public void Update() {
        transform.position = UnityEngine.Camera.main.WorldToScreenPoint(monster.transform.position + new Vector3(0, headOffset, 0));

        float monsterMaxHealth = monster.GetComponent<Monster>().maxHealth;
        float monsterCurrentHealth = monster.GetComponent<Monster>().health;
        slider.value = monsterCurrentHealth / monsterMaxHealth;
    }
}
