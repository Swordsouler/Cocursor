using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ClickableArea : MonoBehaviourPunCallbacks, IPunObservable {
    public Color originColor;

    [SerializeField]
    private int maxNumberOfClicks = 100;
    private int numberOfClicks = 0;

    [SerializeField]
    private TextMesh text;
    [SerializeField]
    private SpriteRenderer background;

    private void Start() {
        background.color = originColor;
        numberOfClicks = maxNumberOfClicks;

        //adapt the size of the text
        float ratio = transform.localScale.x / transform.localScale.y;
        text.transform.localScale = new Vector3(0.05f / ratio / transform.localScale.y, 0.05f * ratio / transform.localScale.x, 0.05f);
    }

    private void Update() {
        text.text = numberOfClicks <= 0 ? "" : numberOfClicks.ToString();
        background.color = new Color(originColor.r, originColor.g, originColor.b, (float)numberOfClicks / (float)maxNumberOfClicks);
        if(Input.GetKeyDown(KeyCode.Space)) {
            performClick();
        }
    }

    public void performClick() {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("Click", RpcTarget.All);
    }

    [PunRPC]
    void Click() {
        numberOfClicks--;
        if(numberOfClicks == 0) {
            StartCoroutine(Respawn());
        }
    }

    public bool isFinish() {
        return numberOfClicks <= 0;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            // We own this player: send the others our data
            stream.SendNext(numberOfClicks);
        } else {
            // Network player, receive data
            this.numberOfClicks = (int)stream.ReceiveNext();
        }
    }

    private IEnumerator Respawn() {
        yield return new WaitForSeconds(5f);
        numberOfClicks = maxNumberOfClicks;
    }
}
