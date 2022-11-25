using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour {
    public GameObject basicRoom0, basicRoom1, basicRoom2, basicEndRoom, basicTreaureRoom;
    public Room currentRoom;

    private Map map;
    private int globalRoomId;
    private int maxNumRooms;
    private int totalNumRooms;

    public void Start() {
        GameObject startRoom = Instantiate(basicRoom0);

        startRoom.GetComponent<Room>().position = new Vector2Int(0, 0);
        List<GameObject> roomPrefabs = new List<GameObject>() {
            basicRoom0, basicRoom1, basicRoom2, basicEndRoom, basicTreaureRoom
        };
        map = new Map(startRoom.GetComponent<Room>(), roomPrefabs, 2);
        map.constructMapTree(2);

        currentRoom = map.rootRoom;
        setRoomActive(currentRoom);

        globalRoomId = 0;
        maxNumRooms = 10;
        totalNumRooms = 0;
    }

    public void Update() {

    }

    public void setRoomActive(Room room) {
        room.gameObject.SetActive(true);
    }

    public void setRoomDisable(Room room) {
        room.gameObject.SetActive(false);
    }

    public void proceedUp() {
        if (currentRoom.childRooms[0] != null) {
            setRoomDisable(currentRoom);
            currentRoom = currentRoom.childRooms[0];
            setRoomActive(currentRoom);
        }
        else
            Debug.Log("No way up!");
    }

    public void proceedRight() {
        if (currentRoom.childRooms[1] != null) {
            setRoomDisable(currentRoom);
            currentRoom = currentRoom.childRooms[1];
            setRoomActive(currentRoom);
        }
        else
            Debug.Log("No way right!");
    }

    public void proceedDown() {
        if (currentRoom.childRooms[2] != null) {
            setRoomDisable(currentRoom);
            currentRoom = currentRoom.childRooms[2];
            setRoomActive(currentRoom);
        }
        else
            Debug.Log("No way down!");
    }

    public void proceedLeft() {
        if (currentRoom.childRooms[3] != null) {
            setRoomDisable(currentRoom);
            currentRoom = currentRoom.childRooms[3];
            setRoomActive(currentRoom);
        }
        else
            Debug.Log("No way left!");
    }
}
