using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCollision : MonoBehaviour {
    private List<GameObject> objects = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Clickable") {
            objects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Clickable") {
            objects.Remove(collision.gameObject);
        }
    }

    public void performClick() {
        if (objects.Count > 0) {
            foreach (GameObject obj in objects) {
                obj.GetComponent<ClickableArea>().performClick();
            }
        }
    }
}
