using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HoverableArea : Area {

    private int numberOfCursorOver = 0;

    [SerializeField]
    private SpriteRenderer background;


    private void Awake() {
        if (area) {
            originColor = area.originColor;
        }
    }

    private void Start() {
        background.color = originColor;
    }

    private void Update() {
        background.color = originColor;
        if (area) {
            isShow = area.isFinish();
        }
        background.enabled = isShow;
        gameObject.GetComponent<BoxCollider2D>().enabled = isShow;
    }

    public override bool isFinish() {
        return this.numberOfCursorOver > 0;
    }

    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            // We own this player: send the others our data
            stream.SendNext(numberOfCursorOver);
        } else {
            // Network player, receive data
            this.numberOfCursorOver = (int)stream.ReceiveNext();
        }
    }

    public void performHoverEnter() {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("OnHoverEnter", RpcTarget.All);
    }

    public void performHoverExit() {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("OnHoverExit", RpcTarget.All);
    }

    [PunRPC]
    void OnHoverEnter() {
        numberOfCursorOver++;

    }

    [PunRPC]
    void OnHoverExit()
    {
        numberOfCursorOver--;
    }
}
