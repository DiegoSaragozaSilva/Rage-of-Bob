using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    public List<GameObject> constructionPrefabs;

    private int globalRoomId;

    public void Start() {
        globalRoomId = 0;

        generateRoom();
    }

    public void generateRoom() {
        GameObject roomObj = new GameObject();
        roomObj.AddComponent<Room>();

        Room newRoom = roomObj.GetComponent<Room>();
        newRoom.setId(globalRoomId++);
        newRoom.setWidth(50);
        newRoom.setHeight(90);
        newRoom.setFloorPrefab(constructionPrefabs[0]);
        newRoom.generate();

        roomObj.name = "Room (" + globalRoomId + ")";
    }
}
