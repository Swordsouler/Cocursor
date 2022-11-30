using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class MoveDependingMouse : MonoBehaviourPunCallbacks
{
    public float sensitivity = 2f;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(!photonView.IsMine) return;

        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        if(mouseDelta.magnitude <= 100 && Cursor.lockState == CursorLockMode.Locked) {
            rb.velocity = mouseDelta * sensitivity;
        }
    }
}
