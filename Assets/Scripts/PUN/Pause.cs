using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;

    private void Awake() {
        pauseMenu.SetActive(false);
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
        PlayerManager.localPlayerInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(GameIsPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }
}
