using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour {
    public GameObject player;
    public TextMeshProUGUI percentage;

    private Slider slider;

    public void Start() {
        slider = gameObject.GetComponent<Slider>();
    }

    public void Update() {
        float playerMaxHealth = player.GetComponent<Player>().maxHealth;
        float playerCurrentHealth = player.GetComponent<Player>().health;
        slider.value = playerCurrentHealth / playerMaxHealth;

        string percentageText = (100.0f * slider.value) + " %";
        percentage.text = percentageText;
    }
}
