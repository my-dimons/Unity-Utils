using UnityEngine;

namespace UnityUtils.ScriptUtils.Cameras {
  public class CameraBillboard : MonoBehaviour {
    /// <summary>
    /// If true, will ignore the selected camera and use Camera.main.
    /// </summary>
    public bool useMainCamera;

    [Space(10)]

    /// <summary>
    /// Camera to look at
    /// </summary>
    public Camera billboardCamera;

    /// <summary>
    /// Extra angle to rotate at, only change if the default is not working
    /// </summary>
    public float extraAngleRotation = 180;

    void Update() {
      if (useMainCamera)
        billboardCamera = Camera.main;
    }

    void LateUpdate() {
      LookAtCamera();
    }

    private void LookAtCamera() {
      Transform cameraToLookAt = useMainCamera ? Camera.main.transform : billboardCamera.transform;
      transform.LookAt(cameraToLookAt);
      transform.Rotate(0, extraAngleRotation, 0);
    }
  }
}