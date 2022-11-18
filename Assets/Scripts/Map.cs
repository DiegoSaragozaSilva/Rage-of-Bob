using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
    private Room rootRoom;
    private List<GameObject> roomPrefabs;
    private List<GameObject> northRooms, eastRooms, southRooms, westRooms;
    private List<string> directions;
    private int maxTreasureRooms;

    public Map(Room rootRoom, List<GameObject> roomPrefabs, int maxTreasureRooms) {
        this.rootRoom = rootRoom;
        this.roomPrefabs = roomPrefabs;
        this.maxTreasureRooms = maxTreasureRooms;

        northRooms = new List<GameObject>();
        eastRooms = new List<GameObject>();
        southRooms = new List<GameObject>();
        westRooms = new List<GameObject>();

        directions = new List<string>() {
            "North", "East", "South", "West"
        };

        foreach(GameObject roomPrefab in roomPrefabs) {
            Room room = roomPrefab.GetComponent<Room>();
            if (room.isDirectionFree("North")) northRooms.Add(roomPrefab);
            if (room.isDirectionFree("East")) eastRooms.Add(roomPrefab);
            if (room.isDirectionFree("South")) southRooms.Add(roomPrefab);
            if (room.isDirectionFree("West")) westRooms.Add(roomPrefab);
        }
    }

    public void constructMapTree(int maxDepth) {
        rootRoom.height = 1;
        openRoomNode(rootRoom, 0, maxDepth, 0);
        addExitRoom();

    }

    private void openRoomNode(Room roomNode, int depth, int maxDepth, int numTreasureRooms) {
        if (depth >= maxDepth)
            return;

        int numToGenerateRooms = Random.Range(0, roomNode.connectorDirections.Count);
        for (int i = 0; i < numToGenerateRooms; i++) {
            redo:
            string toOpenDirection = roomNode.connectorDirections[Random.Range(0, roomNode.connectorDirections.Count)];
            if (roomNode.isRoomSpaceFree(toOpenDirection)) {
                GameObject toOpenRoom;
                Vector2Int openedRoomPosition = roomNode.position;
                string opositeDirection = getOpositeDirection(toOpenDirection);
                if (opositeDirection == "North") {
                    toOpenRoom = northRooms[Random.Range(0, northRooms.Count)];
                    openedRoomPosition.y -= 1;
                }
                else if (opositeDirection == "East") {
                    toOpenRoom = eastRooms[Random.Range(0, eastRooms.Count)];
                    openedRoomPosition.x -= 1;
                }
                else if (opositeDirection == "South") {
                    toOpenRoom = southRooms[Random.Range(0, southRooms.Count)];
                    openedRoomPosition.y += 1;
                }
                else {
                    toOpenRoom = westRooms[Random.Range(0, westRooms.Count)];
                    openedRoomPosition.x += 1;
                }

                if (toOpenRoom.GetComponent<Room>().isExit || toOpenRoom.GetComponent<Room>().isTreaure) goto redo;
                else {
                    numTreasureRooms += toOpenRoom.GetComponent<Room>().isTreaure == true ? 1 : 0;
                    GameObject openedRoom = Object.Instantiate(toOpenRoom);
                    roomNode.addChildRoom(openedRoom.GetComponent<Room>(), toOpenDirection);
                    openedRoom.GetComponent<Room>().addChildRoom(roomNode, opositeDirection);
                    openedRoom.GetComponent<Room>().position = openedRoomPosition;
                    openedRoom.GetComponent<Room>().height = depth + 1;
                    openRoomNode(openedRoom.GetComponent<Room>(), depth++, maxDepth, numTreasureRooms);
                }
            }
            else
                goto redo;
        }
    }

    private void addExitRoom() {
        Room farthestRoom = getFarthestRoom(rootRoom);
    }

    private Room getFarthestRoom(Room farthestRoom) {
        Debug.Log(farthestRoom.height);
        foreach(Room childRoom in farthestRoom.childRooms) {
            if (childRoom != null && farthestRoom != childRoom) {
                if (farthestRoom.height < childRoom.height) {
                    farthestRoom = childRoom;
                    return getFarthestRoom(farthestRoom);
                }
            }
        }
        return farthestRoom;
    }

    private string getOpositeDirection(string direction) {
        if (direction == "North") return "South";
        else if (direction == "East") return "West";
        else if (direction == "South") return "North";
        else return "East";
    }
}
