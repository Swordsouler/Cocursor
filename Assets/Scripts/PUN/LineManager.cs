using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LineManager : MonoBehaviourPunCallbacks, IPunObservable {

    private List<Vector2> points;
    private bool isLineFinished = false;

    private IEnumerator Destroy() {
        yield return new WaitForSeconds(30);
        PhotonNetwork.Destroy(gameObject);
    }

    public void setLinePoints(List<Vector2> points) {
        if(!photonView.IsMine) return;
        this.points = points;
        UpdateLine();
    }

    private List<Vector3> Vector2ToVector3(List<Vector2> points) {
        List<Vector3> newPoints = new List<Vector3>();
        foreach(Vector2 point in points) {
            newPoints.Add(point);
        }
        return newPoints;
    }

    private void UpdateLine() {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = this.points.Count;
        lineRenderer.SetPositions(Vector2ToVector3(this.points).ToArray());
    }

    public void FinishLine() {
        isLineFinished = true;
        UpdateLine();
        if(photonView.IsMine) {
            StartCoroutine(Destroy());
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            // We own this player: send the others our data
            stream.SendNext(isLineFinished);
            stream.SendNext(points.Count);
            foreach(Vector2 point in points) {
                stream.SendNext(point);
            }
        } else {
            // Network player, receive data
            if(isLineFinished) return;
            this.isLineFinished = (bool)stream.ReceiveNext();
            int count = (int)stream.ReceiveNext();
            this.points = new List<Vector2>();
            for(int i = 0; i < count; i++) {
                points.Add((Vector2)stream.ReceiveNext());
            }
            if(this.isLineFinished) {
                FinishLine();
            } else {
                UpdateLine();
            }
        }
    }
}
