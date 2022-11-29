using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour {
    public GameObject basicRoom0, basicRoom1, basicRoom2, basicEndRoom, basicTreaureRoom;
    public Room currentRoom;
    public GameObject player;

    private Map map;

    public void Start() {
        GameObject startRoom = Instantiate(basicRoom0);

        startRoom.GetComponent<Room>().position = new Vector2Int(0, 0);
        List<GameObject> roomPrefabs = new List<GameObject>() {
            basicRoom0, basicRoom1, basicRoom2, basicEndRoom, basicTreaureRoom
        };
        map = new Map(startRoom.GetComponent<Room>(), roomPrefabs, 2);
        map.constructMapTree(2);

        currentRoom = map.rootRoom;
        currentRoom.show();
    }

    public void Update() {

    }

    public void moveToRoom(Room room, Vector3 newPlayerPosition) {
        currentRoom.hide();
        currentRoom = room;
        currentRoom.show();

        player.transform.position = Vector3.zero;
    }
}
