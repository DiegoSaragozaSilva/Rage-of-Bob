using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomStatus : MonoBehaviour {

    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    private Image lockImage;

    public void Start() {
        lockImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void Update() {
        RoomManager roomManager = GameObject.Find("Room Manager").GetComponent<RoomManager>();
        if (roomManager.currentRoom.isLocked)
            lockImage.sprite = lockedSprite;
        else
            lockImage.sprite = unlockedSprite;
    }

}
