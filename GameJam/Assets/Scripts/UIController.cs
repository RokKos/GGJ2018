using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] GameController GameController;
    [SerializeField] GameObject MainCanvas;
    [SerializeField] GameObject InfoPanel;
    [SerializeField] Image EndImage;
    [SerializeField] Sprite EndSprite;

    const float CountDownTimer = 3.0f;
    float CurrentTime = 0;
    bool coundownStarted = false;
    bool InfoOn = false;


    private void Start () {
        MainCanvas.SetActive(false);
        coundownStarted = false;
        InfoPanel.SetActive(InfoOn);
        EndImage.enabled = false;
    }
    private void Update () {
        if (Input.GetKeyDown(KeyCode.E)) {
            ShowInfo();
        }


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
        // Deselect button because space would trigger it
        EventSystem.current.SetSelectedGameObject(null, null);
    }

    public void StartQuitGame () {
        Debug.Log("Started Quit");
        MainCanvas.SetActive(true);
        coundownStarted = true;
        EndImage.enabled = true;
        EndImage.sprite = EndSprite;
    }

    private void QuitGame () {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void ShowInfo () {
        InfoOn = !InfoOn;
        InfoPanel.SetActive(InfoOn);
        MainCanvas.SetActive(InfoOn);
    }
}
