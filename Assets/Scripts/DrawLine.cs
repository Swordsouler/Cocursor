using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {
    private GameObject currentLine;
    private List<Vector2> fingerPositions;
    private LineRenderer lineRenderer;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        fingerPositions = new List<Vector2>();
    }

    public void Update() {
        if(Input.GetMouseButtonDown(1)) {
            CreateLine();
        }
        if(Input.GetMouseButton(1)) {
            Vector2 tempFingerPos = PlayerManager.localPlayerInstance.GetComponent<PlayerManager>().getCursorEdgePosition();
            if(Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f) {
                UpdateLine(tempFingerPos);
            }
        }
    }

    private void CreateLine() {
        currentLine = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Line"), Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPositions.Clear();
        fingerPositions.Add(PlayerManager.localPlayerInstance.GetComponent<PlayerManager>().getCursorEdgePosition());
        fingerPositions.Add(PlayerManager.localPlayerInstance.GetComponent<PlayerManager>().getCursorEdgePosition());
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }

    private void UpdateLine(Vector2 newFingerPos) {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }
}
