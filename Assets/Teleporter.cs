using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

  public bool withinRange = false;
  public Teleporter linked;
  GameManager gameManager;
  WorldRotate worldRotate;
  public int unlockReq = 0;
  private bool unlocked;
  public float orientation;
  AudioSource sound;
  public AudioClip badSound;
  public AudioClip goodSound;

    // Start is called before the first frame update
    void Start()
    {
      gameManager = GameManager.gameInstance;
      worldRotate = WorldRotate.worldRotate;
      sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if(!unlocked && gameManager.followers.Count >= unlockReq) {
        if(unlockReq > 0) {
          StartCoroutine(gameManager.displayMessage("You sense a teleporter has been activated somewhere."));
        }
        unlocked = true;
      }
      if(withinRange) {
        if(Input.GetKeyDown("e")) {
          if(unlocked) {
            sound.clip = goodSound;
            sound.Play();
            Teleport();
          } else {
            sound.clip = badSound;
            sound.Play();
            StartCoroutine(gameManager.displayMessage("This teleporter doesn't seem to be active."));
          }
        }
      }
    }

    void OnTriggerEnter(Collider c) {
      if(c.tag != "Player") {
        return;
      }
      withinRange = true;

    }

    void OnTriggerExit(Collider c) {
      if(c.tag != "Player") {
        return;
      }
      withinRange = false;

    }

    void Teleport() {
      float realCurrent = (worldRotate.currentRotation + 360f) % 360;
      float desiredRotation = linked.orientation - realCurrent;
      if(desiredRotation > 180) {
        desiredRotation += -360;
      } else if (desiredRotation < -180) {
        desiredRotation += 360;
      }

      gameManager.followerObject.SetActive(false);
      gameManager.player.SetActive(false);

      gameManager.player.transform.localPosition = new Vector3(linked.transform.position.x, linked.transform.position.y,0);

      worldRotate.cam.arrived = false;
      worldRotate.Rotate(gameManager.player.transform.position, (desiredRotation / 90f), true);
      withinRange = false;
    }
}
