using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
    public Room rootRoom;
    
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
        openRoomNode(rootRoom, 1, maxDepth);
        addExitRoom();
    }

    private void openRoomNode(Room roomNode, int depth, int maxDepth) {
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
                    GameObject openedRoom = Object.Instantiate(toOpenRoom);
                    roomNode.addChildRoom(openedRoom.GetComponent<Room>(), opositeDirection);
                    openedRoom.GetComponent<Room>().addChildRoom(roomNode, toOpenDirection);
                    openedRoom.GetComponent<Room>().position = openedRoomPosition;
                    openedRoom.GetComponent<Room>().height = depth + 1;
                    openRoomNode(openedRoom.GetComponent<Room>(), depth++, maxDepth);
                }
            }
            else goto redo;
        }
    }

    private void addExitRoom() {
        Room farthestRoom = getFarthestRoom(rootRoom);
        foreach (string direction in directions) {
            if (farthestRoom.isDirectionFree(direction) && farthestRoom.childRooms[directionToIndex(direction)] == null) {
                Vector2Int openedRoomPosition = farthestRoom.position;
                foreach (GameObject roomPrefab in roomPrefabs) {
                    if (roomPrefab.GetComponent<Room>().isExit) {
                        GameObject exitPrefab = roomPrefab;

                        string opositeDirection = getOpositeDirection(direction);
                        if (opositeDirection == "North") openedRoomPosition.y -= 1;
                        else if (opositeDirection == "East") openedRoomPosition.x -= 1;
                        else if (opositeDirection == "South") openedRoomPosition.y += 1;
                        else openedRoomPosition.x += 1;

                        GameObject openedRoom = Object.Instantiate(exitPrefab);
                        farthestRoom.addChildRoom(openedRoom.GetComponent<Room>(), direction);
                        openedRoom.GetComponent<Room>().addChildRoom(farthestRoom, opositeDirection);
                        openedRoom.GetComponent<Room>().position = openedRoomPosition;
                        openedRoom.GetComponent<Room>().height = farthestRoom.height + 1;
                        return;
                    }
                }
            }
        }
    }

    private Room getFarthestRoom(Room room) {      
        int childCount = 0;
        foreach (Room childRoom in room.childRooms) {
            if (childRoom != null) childCount++;
            if (childCount > 1) break;
        }
        if (childCount == 1) return room;

        foreach (Room childRoom in room.childRooms) {
            if (childRoom != null && childRoom.height > room.height) {
                Room _room = getFarthestRoom(childRoom);
                if (_room.height > childRoom.height) return _room;
                else return childRoom;
            }
        }

        return room;
    }

    private string getOpositeDirection(string direction) {
        if (direction == "North") return "South";
        else if (direction == "East") return "West";
        else if (direction == "South") return "North";
        else return "East";
    }

    private int directionToIndex(string direction) {
        if (direction == "North") return 0;
        else if (direction == "East") return 1;
        else if (direction == "South") return 2;
        else return 3;
    }
}
