using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {
    public void Start() {
        Time.timeScale = 1;
    }
    public void callGameOver() {
        transform.Find("Gameover").gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void callWin()
    {
        transform.Find("Win").gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void restartGame() {
        SceneManager.LoadScene("Game");
    }
}
