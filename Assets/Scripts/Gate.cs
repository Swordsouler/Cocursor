using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gate : MonoBehaviourPunCallbacks, IPunObservable {
    
    private bool isGateOpen = false;
    [SerializeField]
    private ClickableArea clickableArea;

    private void Start() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(
            clickableArea.originColor.r, 
            clickableArea.originColor.g, 
            clickableArea.originColor.b, 
            1f
        );
    }

    private void Update() {
        if(photonView.IsMine) {
            if(clickableArea.isFinish()) {
                isGateOpen = true;
            } else {
                isGateOpen = false;
            }
        }
        if(isGateOpen) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } else {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // We own this player: send the others our data
            stream.SendNext(isGateOpen);
            stream.SendNext(isGateOpen);
        } else {
            // Network player, receive data
            this.isGateOpen = (bool)stream.ReceiveNext();
        }
    }
}
