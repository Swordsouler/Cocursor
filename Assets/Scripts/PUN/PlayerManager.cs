using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

using System.Collections;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable {

    [SerializeField]
    private SpriteRenderer bodyRenderer;

    private bool isPressing = false;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public void Awake() {
        if (photonView.IsMine) {
            PlayerManager.LocalPlayerInstance = this.gameObject;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Application.targetFrameRate = 60;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {
        if (photonView.IsMine) {
            ProcessInputs();
        }
        if(isPressing) {
            bodyRenderer.color = Color.red;
        } else {
            bodyRenderer.color = Color.black;
        }
    }

    void ProcessInputs() {
        isPressing = Input.GetButton("Fire1");
        
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            // We own this player: send the others our data
            stream.SendNext(isPressing);
        } else {
            // Network player, receive data
            this.isPressing = (bool)stream.ReceiveNext();
        }
    }
}