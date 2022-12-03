using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
    public float fadeSpeed;

    private RawImage fadeImage;

    public void Start() {
        fadeImage = gameObject.GetComponent<RawImage>();
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator Fade(bool fadeAway) {
        if (fadeAway) {
            for (float i = 0; i < 1.5f; i += fadeSpeed * Time.deltaTime) {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        else
            for (float i = 1.0f; i > -0.5f; i -= fadeSpeed * Time.deltaTime) {
                fadeImage.color = new Color(0, 0, 0, i);
                yield return null;
            }

        if (fadeAway) StartCoroutine(Fade(false));
    }
}
