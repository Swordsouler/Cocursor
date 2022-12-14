using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCollision : MonoBehaviour {
    private List<GameObject> objects = new List<GameObject>();
    private GameObject checkPoint = null;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        switch(collision.gameObject.tag) {
            case "Clickable":
                objects.Add(collision.gameObject);
                break;
            case "Hoverable":
                collision.gameObject.GetComponent<HoverableArea>().performHoverEnter();
                break;
            case "Check Point":
                checkPoint = collision.gameObject;
                break;
            case "Kill":
                gameObject.GetComponent<PlayerManager>().respawnPlayer();
                break;
            case "Portal":
                collision.gameObject.GetComponent<Portal>().teleportPlayer(gameObject.GetComponent<PlayerManager>());
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        switch(collision.gameObject.tag) {
            case "Clickable":
                objects.Remove(collision.gameObject);
                break;
            case "Hoverable":
                collision.gameObject.GetComponent<HoverableArea>().performHoverExit();
                break;
            case "Check Point":
                break;
        }
    }

    public void performClick() {
        if (objects.Count > 0) {
            foreach (GameObject obj in objects) {
                obj.GetComponent<ClickableArea>().performClick();
            }
        }
    }

    public Vector3 getCheckPointPosition() {
        if(checkPoint != null) {
            return new Vector3(checkPoint.transform.position.x, checkPoint.transform.position.y, gameObject.transform.position.z);
        }
        return Vector3.zero;
    }
}
