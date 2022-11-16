using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {
    public List<string> connectorDirections;
    public int id;
    public bool isTreaure;
    public bool isExit;
    
    public void Start() {
        gameObject.SetActive(false);
    }

    public void setId(int id) {
        this.id = id;
    }

    public bool isDirectionFree(string direction) {
        return connectorDirections.Contains(direction);
    }

    public void show() {
        transform.parent.gameObject.SetActive(true);
    }

    public void hide() {
        transform.parent.gameObject.SetActive(false);
    }

    public void destroy() {
        Destroy(transform.parent.gameObject);
    }
}
