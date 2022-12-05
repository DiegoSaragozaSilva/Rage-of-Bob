using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour {
    public GameObject basicRoom0, basicRoom1, basicRoom2, basicEndRoom, basicTreaureRoom;
    public Room currentRoom;
    public GameObject player;
    public GameObject fader;
    public GameObject bossNoticeMessage;

    private Map map;
    private SoundManager soundManager;


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

        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    public void Update() {
        // Unlock room
        if (currentRoom.isLocked && currentRoom.totalMonsters == 0) {
            currentRoom.isLocked = false;
            player.GetComponent<Player>().health += 5;
            soundManager.playSound(soundManager.roomUnlock);
        }
    }

    public void moveToRoom(Room room, Vector3 newPlayerPosition) {
        bool isMapCompleted = map.isCompleted();
        if (room.isExit && !isMapCompleted) {
            bossNoticeMessage.GetComponent<MovementAnimator>().animate();
            soundManager.playSound(soundManager.roomLocked);
            return;
        }

        if (isMapCompleted) soundManager.playSound(soundManager.bossUnlock);

        if (!currentRoom.isLocked) {
            StartCoroutine(fader.GetComponent<Fader>().Fade(true));

            currentRoom.hide();
            currentRoom = room;
            currentRoom.show();

            player.transform.position = Vector3.zero;
        }
        else soundManager.playSound(soundManager.roomLocked);
    }

    public void notifyMonsterSpawn() {
        if (currentRoom.totalMonsters == -1) currentRoom.totalMonsters = 0;
        currentRoom.totalMonsters++;
    }

    public void notifyMonsterKill() {
        currentRoom.totalMonsters--;
    }
}
