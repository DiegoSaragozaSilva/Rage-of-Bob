using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {
    public int id;
    public bool isTreaure;
    public bool isExit;
    public List<string> connectorDirections;
    public Vector2Int position;
    public int height;
    public List<Room> childRooms;
    
    public void Awake() {
        childRooms = new List<Room>() {
            null, null, null, null
        };

        gameObject.SetActive(false);
    }

    public void setId(int id) {
        this.id = id;
    }

    public bool isDirectionFree(string direction) {
        return connectorDirections.Contains(direction);
    }

    public bool isRoomSpaceFree(string direction) {
        if (direction == "North")
            return childRooms[0] is null;
        else if (direction == "East")
            return childRooms[1] is null;
        else if (direction == "South")
            return childRooms[2] is null;
        else
            return childRooms[3] is null;
    }

    public void addChildRoom(Room room, string direction) {
        if (direction == "North")
            childRooms[0] = room;
        else if (direction == "East")
            childRooms[1] = room;
        else if (direction == "South")
            childRooms[2] = room;
        else
            childRooms[3] = room;
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
