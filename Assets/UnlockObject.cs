using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockObject : MonoBehaviour
{

  public int unlockReq;
  public GameObject unlockParticle;
  public CameraFollow cam;
  public bool unlocked;
  public GameObject destroyObject;
  AudioSource sound;

  GameManager gameManager;
    void Start()
    {
      gameManager = GameManager.gameInstance;
      sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      if((gameManager.followers.Count == unlockReq) && !unlocked) {
        cam.SetTarget(transform, true);
        unlocked = true;
        StartCoroutine(Unlock());
      }
    }

    IEnumerator Unlock() {
      while(cam.arrived == false) {
        yield return null;
      }
      GameObject g = (GameObject)Instantiate(unlockParticle, transform.position, Quaternion.identity);
      sound.Play();
      Destroy(destroyObject);
      Destroy(g, 5f);
      yield return new WaitForSeconds(3);
      cam.SetTarget(gameManager.player.transform, true);
      Destroy(gameObject);
    }

}
