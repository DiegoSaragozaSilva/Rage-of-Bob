using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource bullet;
    public AudioSource enemyAttack;
    public AudioSource enemyHurt;
    public AudioSource roomUnlock;
    public AudioSource roomLocked;
    public AudioSource bossUnlock;

    public void playSound(AudioSource sound) {
        sound.Play();
    }
}
