using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject helpMenu;

    public void callHelpMenu() {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void callMainMenu() {
        helpMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void callPlayScene() {
        SceneManager.LoadScene("Game");
    }
}
