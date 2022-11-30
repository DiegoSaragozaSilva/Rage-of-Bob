using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField]
    public float spawnChance = 0.0f;
    public GameObject monsterPrefab;

    public void Start() {
        float rng = Random.Range(0.0f, 1.0f);
        Debug.Log(rng);
        if (rng <= spawnChance)
            Instantiate(monsterPrefab, transform.position, transform.rotation);
    }
}
