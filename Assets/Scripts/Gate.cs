using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gate : MonoBehaviourPunCallbacks, IPunObservable {
    public enum GateType {
        OR,
        AND
    }
    public bool isGateOpen = false;
    [SerializeField]
    private Area[] areas = new Area[0];

    [SerializeField]
    private GateType gateType = GateType.OR;

    [SerializeField]
    private bool isInverted = false;

    private void Start() {
        SpriteRenderer spriteRenderer;
        if(gameObject.TryGetComponent<SpriteRenderer>(out spriteRenderer)) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(
                areas[0].originColor.r, 
                areas[0].originColor.g, 
                areas[0].originColor.b, 
                1f
            );
        }
    }

    private void Update() {
        if(photonView.IsMine) {
            switch(gateType) {
                case GateType.OR:
                    foreach(Area area in areas) {
                        isGateOpen = area.isFinish();
                        if(isGateOpen) break;
                    }
                    break;
                case GateType.AND:
                    isGateOpen = true;
                    foreach(Area area in areas) {
                        isGateOpen = area.isFinish();
                        if(!isGateOpen) break;
                    }
                    break;
            }
        }
        if(isGateOpen) {
            SpriteRenderer spriteRenderer;
            if(gameObject.TryGetComponent<SpriteRenderer>(out spriteRenderer)) {
                spriteRenderer.enabled = isInverted;
            }
            BoxCollider2D boxCollider2D;
            if(gameObject.TryGetComponent<BoxCollider2D>(out boxCollider2D)) {
                boxCollider2D.enabled = isInverted;
            }
            MeshRenderer meshRenderer;
            if(gameObject.TryGetComponent<MeshRenderer>(out meshRenderer)) {
                meshRenderer.enabled = isInverted;
            }
        } else {
            SpriteRenderer spriteRenderer;
            if(gameObject.TryGetComponent<SpriteRenderer>(out spriteRenderer)) {
                spriteRenderer.enabled = !isInverted;
            }
            BoxCollider2D boxCollider2D;
            if(gameObject.TryGetComponent<BoxCollider2D>(out boxCollider2D)) {
                boxCollider2D.enabled = !isInverted;
            }
            MeshRenderer meshRenderer;
            if(gameObject.TryGetComponent<MeshRenderer>(out meshRenderer)) {
                meshRenderer.enabled = !isInverted;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // We own this player: send the others our data
            stream.SendNext(isGateOpen);
        } else {
            // Network player, receive data
            this.isGateOpen = (bool)stream.ReceiveNext();
        }
    }
}
