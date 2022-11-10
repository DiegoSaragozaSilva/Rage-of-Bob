using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public int id, width, height;
    public GameObject floorPrefab;

    public void setId(int id) {
        this.id = id;
    }

    public void setWidth(int width) {
        this.width = width;
    }

    public void setHeight(int height) {
        this.height = height;
    }

    public void setFloorPrefab(GameObject floorPrefab) {
        this.floorPrefab = floorPrefab;
    }

    public void generate() {
        for (int j = 0; j < height; j++)
            for (int i = 0; i < width; i++) {
                GameObject floor = Instantiate(floorPrefab, new Vector3(i, j, 0), transform.rotation, transform);
            }
    }
}
