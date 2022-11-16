using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour {
    public GameObject basicRoom0, basicRoom1, basicRoom2, basicEndRoom, basicTreaureRoom;

    private int globalRoomId;
    private int maxNumRooms;
    private int totalNumRooms;
    private int[,] map;
    private List<GameObject> mapRooms;

    private float treasureRoomChange = 0.25f;

    public void Start() {
        globalRoomId = 0;
        maxNumRooms = 10;
        totalNumRooms = 0;
        map = new int[maxNumRooms, maxNumRooms];
        mapRooms = new List<GameObject>();

        generateMap();

        for (int j = 0; j < maxNumRooms; j++) {
            string aux = "";
            for (int i = 0; i < maxNumRooms; i++) {
                aux += map[i, j] + " ";
            }
            Debug.Log(aux);
        }
    }

    public void generateMap() {
        Vector2Int firstRoomPosition = new Vector2Int(Random.Range(0, maxNumRooms), Random.Range(0, maxNumRooms));
        map[firstRoomPosition.x, firstRoomPosition.y] = 1;

        int currentId = 2;
        Vector2Int currentPosition = firstRoomPosition;
        while (totalNumRooms < maxNumRooms) {
            Vector2Int nextPosition = new Vector2Int();
            int direction = Random.Range(0, 3);
            if (direction == 0)
                nextPosition = new Vector2Int(currentPosition.x, currentPosition.y + 1);
            else if (direction == 1)
                nextPosition = new Vector2Int(currentPosition.x + 1, currentPosition.y);
            else if (direction == 2)
                nextPosition = new Vector2Int(currentPosition.x, currentPosition.y - 1);
            else if (direction == 3)
                nextPosition = new Vector2Int(currentPosition.x - 1, currentPosition.y);

            if (isPositionValid(nextPosition) && isCellEmpty(nextPosition)) {
                map[nextPosition.x, nextPosition.y] = currentId;

                currentPosition = nextPosition;
                currentId++;
                totalNumRooms++;
            }
        }
    }

    public bool isPositionValid(Vector2Int position) {
        return (position.x >= 0 && position.x < maxNumRooms) && (position.y >= 0 && position.y < maxNumRooms);
    }

    public bool isCellEmpty(Vector2Int position) {
        return map[position.x, position.y] == 0;
    }

    public GameObject generateRoom(GameObject roomPrefab) {
        GameObject roomObj = Instantiate(roomPrefab);
        Room newRoom = roomObj.GetComponent<Room>();
        roomObj.name = "Room (" + globalRoomId + ")";
        newRoom.setId(globalRoomId++);
        return roomObj;
    }
}
