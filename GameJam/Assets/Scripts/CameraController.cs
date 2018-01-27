using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Camera Camera;
    [SerializeField] AnimationCurve AnimationCurveZoomFactor;
    [SerializeField] AnimationCurve AnimationCurvePositionX;
    [SerializeField] AnimationCurve AnimationCurvePositionY;
    const float ZoomInScale = 0.5f;
    const float ZoomOutScale = 5f;
    const float TimeToZoomOut = 3f;
    float CurTime = 0;

    // Update is called once per frame
    void Update () {
        ZoomOutCamera();
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

        float xCordinate = AnimationCurvePositionX.Evaluate(CurTime / TimeToZoomOut);
        float yCordinate = AnimationCurvePositionY.Evaluate(CurTime / TimeToZoomOut);

    }
}
