using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Area : MonoBehaviourPunCallbacks, IPunObservable {
    public Color originColor;
    public Area area;
    public bool isShow = true;

    public abstract bool isFinish();
    public abstract void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info);
}
