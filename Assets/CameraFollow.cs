using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Transform target;
  public float smoothSpeed = 0.2f;
  public Vector3 offset;
  private Vector3 velocity = Vector3.zero;
  public bool arrived = true; //used for events that require the camera to arrive before things execute.

  void Start() {
    offset = new Vector3(0, 0, -10);
  }

  void LateUpdate() {
    Vector3 desiredPosition = target.position + offset;
    Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.35f);
    transform.position = smoothedPosition;
    if(Vector3.Distance(target.position, transform.position - offset) < 0.1) {
      arrived = true;
    }
  }

  public void SetTarget(Transform pos, bool useArrive) {
    target = pos;
    if(useArrive) {
      arrived = false;
    }
  }
}
