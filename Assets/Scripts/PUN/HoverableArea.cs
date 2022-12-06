using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HoverableArea : MonoBehaviourPunCallbacks, IPunObservable {
    public Color originColor;

    private bool isHovered = false;
    [SerializeField]
    private HoverableArea hoverableArea2; // will spawn this second hoverable area when this current hoverable area is hovered

    [SerializeField]
    private SpriteRenderer background;

    private void Start() {
        background.color = originColor;
        if (hoverableArea2)
        {
            hoverableArea2.gameObject.SetActive(false);
        }
    }

    private void Update() {
        background.color = new Color(originColor.r, originColor.g, originColor.b);
        if(Input.GetKeyDown(KeyCode.Space)) {
            OnHoverEnter();
        }
    }

    public bool isFinish()
    {
        if (hoverableArea2)
        {
            return hoverableArea2.isHovered;
        }
        else
        {
            return false;
        }
    }



        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            // We own this player: send the others our data
            stream.SendNext(isHovered);
        } else {
            // Network player, receive data
            this.isHovered = (bool)stream.ReceiveNext();
        }
    }

    public void performHoverEnter()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("OnHoverEnter", RpcTarget.All);
    }

    public void performHoverExit()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("OnHoverExit", RpcTarget.All);
    }

    [PunRPC]
    void OnHoverEnter()
    {
        // Example later implement CanPlayerDoThis()
        // Maybe Team A can do this but not Team B
        GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 1);
        if (hoverableArea2)
        {
            hoverableArea2.gameObject.SetActive(true);
        }

    }

    [PunRPC]
    void OnHoverExit()
    {
        // Example later implement CanPlayerDoThis()
        // Maybe Team A can do this but not Team B
        GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0, 1);
        if (hoverableArea2)
        {
            hoverableArea2.gameObject.SetActive(false);
        }

    }
}
