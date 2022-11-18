using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour {
    public GameObject basicRoom0, basicRoom1, basicRoom2, basicEndRoom, basicTreaureRoom;

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
        map.constructMapTree(5);

        globalRoomId = 0;
        maxNumRooms = 10;
        totalNumRooms = 0;
    }
}
