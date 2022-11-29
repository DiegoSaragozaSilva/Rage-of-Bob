using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public int direction;

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            transform.parent.gameObject.GetComponent<Room>().sendDoorTrigger(direction, transform.position);
        }
    }
}
