using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementAnimator : MonoBehaviour {
    public Vector3 targetOffset;
    public float holdTime;

    public void animate() {
        transform.position = transform.position + targetOffset;
        StartCoroutine(wait(holdTime, animateReverse));
    }

    private IEnumerator wait(float time, UnityAction doLast) {
        yield return new WaitForSeconds(time);
        doLast();
    }

    private void animateReverse() {
        transform.position = transform.position - targetOffset;
    }
}
