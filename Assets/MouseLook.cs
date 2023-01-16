using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

  public float sens = 100f;
  float xRotation = 0f;
  float yRotation = 0f;
  public Transform playerBody;
  public Transform playerIcon;


void Start()
{
  Cursor.lockState = CursorLockMode.Locked;
}

  void Update() {
    float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    yRotation += mouseX;

    transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    playerBody.Rotate(Vector3.up * mouseX);
    playerIcon.Rotate(Vector3.back * mouseX);
  }
}
