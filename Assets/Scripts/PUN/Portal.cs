using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    [SerializeField]
    private Transform endPoint;

    private void Start() {
        if (endPoint == null) {
            Destroy(gameObject);
        }
    }

    public void teleportPlayer(PlayerManager player) {
        player.teleportToLoaction(endPoint.position);
    }
}
