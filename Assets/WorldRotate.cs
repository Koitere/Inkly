using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotate : MonoBehaviour
{
  public GameManager gameManager;
  public static WorldRotate worldRotate;
  public Transform world;
  public float currentRotation = 0f;
  public CameraFollow cam;
  AudioSource sound;

  void Awake()
  {
    if(worldRotate != null)
    {
      Debug.LogError ("More than one GameManager in scene!");
      return;
    }
    worldRotate = this;
    sound = gameObject.GetComponent<AudioSource>();
  }

    void Start() {
      gameManager = GameManager.gameInstance;
    }

    public void Rotate(Vector3 around, float direction, bool teleporter) { //direction is multiplied by 90 so -1 would be -90, -2 is -180, 2 is 180 etc.
      gameManager.moving = true;
      if(Mathf.Abs(((90 * direction) / 90) % 2) == 1) {
        gameManager.bitSwitch = true;
      }
      currentRotation += (90f * direction);
      currentRotation = currentRotation % 360;
      StartCoroutine(Rota(around, 90f * direction, teleporter));
    }

    IEnumerator Rota(Vector3 around, float rotation, bool teleporter) {
      float count = 0f;
      while(!cam.arrived) {
        yield return null;
      }
      sound.Play();
      while(count < Mathf.Abs((rotation))) {
        float temp = count;
        count += Mathf.Abs(rotation) * Time.deltaTime;
        if(count > Mathf.Abs((rotation))) {
          if(rotation > 0) {
            temp = rotation - temp;
          } else {
            temp = rotation + temp;
          }
          world.RotateAround(around, Vector3.forward, temp);
        } else {
          world.RotateAround(around, Vector3.forward, rotation * Time.deltaTime);
        }
        yield return null;
      }

      if(teleporter) {
        Vector3 temp = new Vector3(0,0.5f,0);
        gameManager.player.transform.position += temp;
        gameManager.player.SetActive(true);
        gameManager.followerObject.SetActive(true);
      }
      gameManager.moving = false;

    }
}
