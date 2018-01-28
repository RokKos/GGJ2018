using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] GameController GameController;
    [SerializeField] Canvas MainCanvas;
    [SerializeField] Image EndImage;
    [SerializeField] Sprite EndSprite;

    const float CountDownTimer = 3.0f;
    float CurrentTime = 0;
    bool coundownStarted = false;


    private void Start () {
        MainCanvas.enabled = false;
        coundownStarted = false;
        EndImage.enabled = false;
    }
    private void Update () {
        if (!coundownStarted) {
            return;
        }

        CurrentTime += Time.deltaTime;
        if (CurrentTime > CountDownTimer) {
            QuitGame();
        }
    }

    public void RestartTransmition () {
        Debug.Log("Restart");
        GameController.ResetTransmition();
    }

    public void StartQuitGame () {
        Debug.Log("Started Quit");
        MainCanvas.enabled = true;
        coundownStarted = true;
        EndImage.enabled = true;
        EndImage.sprite = EndSprite;
    }

    private void QuitGame () {
        Debug.Log("Quit");
        Application.Quit();
    }
}
