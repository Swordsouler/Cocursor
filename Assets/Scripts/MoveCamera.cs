using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    private int cameraX = 0;
    private int cameraY = 0;

    void Update() {
        if(isCameraMoved()) {
            cameraX = (int) Mathf.Floor((PlayerManager.localPlayerInstance.transform.position.x + 16) / 32);
            cameraY = (int) Mathf.Floor((PlayerManager.localPlayerInstance.transform.position.y + 9.3f) / 18);
        }
        Vector3 cameraPosition = Vector3.Lerp(transform.position, new Vector3(cameraX * 32, cameraY * 18, -10), 0.05f);
        transform.position = cameraPosition;
    }

    private bool isCameraMoved() {
        return Mathf.Floor((PlayerManager.localPlayerInstance.transform.position.x + 16) / 32) != cameraX || 
            Mathf.Floor((PlayerManager.localPlayerInstance.transform.position.y + 9.3f) / 18) != cameraY;
    }
}
