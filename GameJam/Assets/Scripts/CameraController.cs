using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Camera Camera;
    [SerializeField] AnimationCurve AnimationCurveZoomFactor;
    [SerializeField] AnimationCurve AnimationCurvePositionX;
    [SerializeField] AnimationCurve AnimationCurvePositionY;
    [SerializeField] GameController GameController;
    const float ZoomInScale = 8.1f;
    const float ZoomOutScale = 17f;
    const float TimeToZoomOut = 3f;
    const float StartingPosX = -4.18f;
    const float StartingPosY = 5;
    float CurTime = 0;
    

    // Update is called once per frame
    void Update () {
        if (!GameController.GetPastGameMenu()) {
            return;
        }

        ZoomOutCamera();
        PositionCamera();
    }

    void ZoomOutCamera () {
        if (CurTime > TimeToZoomOut) {
            return;
        }
        CurTime += Time.deltaTime;

        float value = AnimationCurveZoomFactor.Evaluate(CurTime / TimeToZoomOut);
        Camera.orthographicSize = Mathf.Lerp(ZoomInScale, ZoomOutScale, value);
    }

    void PositionCamera () {
        if (CurTime > TimeToZoomOut) {
            return;
        }
        CurTime += Time.deltaTime;

        float xValue = AnimationCurvePositionX.Evaluate(CurTime / TimeToZoomOut);
        float posX = Mathf.Lerp(StartingPosX, 0.0f, xValue);
        float yValue = AnimationCurvePositionY.Evaluate(CurTime / TimeToZoomOut);
        float posY = Mathf.Lerp(StartingPosY, 0.0f, yValue);

        Camera.transform.position = new Vector3(posX, posY, -10);

    }
}
