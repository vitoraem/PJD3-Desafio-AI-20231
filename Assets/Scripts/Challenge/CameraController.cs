using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ChallengeAI {
  public class CameraController : MonoBehaviour
  {
    private Transform containerTransform;
    private Transform cameraTransform;
    private void Awake() {
      containerTransform = GetComponent<Transform>();
      cameraTransform = containerTransform.GetChild(0);

      containerTransform.position = new Vector3(0,1,-7);
      containerTransform.rotation = Quaternion.Euler(45f,0,0);
      cameraTransform.localPosition = new Vector3(0,0,-26);
    }
  }
}
