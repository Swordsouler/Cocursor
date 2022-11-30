using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Map : MonoBehaviour {
    private enum TileType {
        Void,
        Wall
    }
    
    public abstract int[,] getMap();

    private void Start() {
        int[,] map = getMap();
        for (int y = 0; y < map.GetLength(0); y++) {
            for (int x = 0; x < map.GetLength(1); x++) {
                if (map[y, x] == 1) {
                    GameObject go = GameObject.Instantiate(
                        Resources.Load<GameObject>("Prefabs/Wall"), 
                        new Vector3(
                            -15.5f + x + gameObject.transform.position.x, 
                            8.5f - y + gameObject.transform.position.y, 
                            2
                        ), 
                        Quaternion.identity);
                    go.transform.SetParent(transform);
                }
            }
        }
    }
}
