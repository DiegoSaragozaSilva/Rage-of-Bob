using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;
    public float lambda;
    
    void Start() {
        lambda = 20;
    }

    void LateUpdate() {
        Vector2 cameraPosition = transform.position;
        Vector2 targetPosition = target.transform.position;
        if (cameraPosition != targetPosition) {
            cameraPosition += (targetPosition - cameraPosition) / lambda;
            transform.position = new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z);
        }
    }
}
