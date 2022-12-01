using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

using System.Collections;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable {
    private CursorCollision cursorCollision;

    [SerializeField]
    private SpriteRenderer bodyRenderer;

    private bool isPressing = false;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject localPlayerInstance;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        if(!photonView.IsMine) {
            GetComponent<CursorCollision>().enabled = false;
            GetComponent<PlayerManager>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Collider2D>().enabled = false;

        }
    }

    public void Awake() {
        cursorCollision = GetComponentInChildren<CursorCollision>();
        if (photonView.IsMine) {
            PlayerManager.localPlayerInstance = this.gameObject;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Application.targetFrameRate = 60;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {
        if(photonView.IsMine) {
            ProcessInputs();
        }
        bodyRenderer.color = isPressing ? Color.red : Color.black;
    }

    void ProcessInputs() {
        if(Pause.GameIsPaused) return;
        isPressing = Input.GetButton("Fire1");
        if(Input.GetButtonDown("Fire1")) {
            cursorCollision.performClick();
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

    public Vector3 getCursorEdgePosition() {
        Vector3 position = transform.position;
        position.x -= 0.25f;
        position.y += 0.25f;
        return position;
    }

    public void respawnPlayer() {
        if (photonView.IsMine) {
            gameObject.transform.position = cursorCollision.getCheckPointPosition();
        }
    }
}