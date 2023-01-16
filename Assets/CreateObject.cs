using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{

  public int unlockReq;
  public GameObject createParticle;
  public CameraFollow cam;
  public bool created;
  public GameObject createPrefab;
  WorldRotate worldRotate;
  GameManager gameManager;
  AudioSource sound;

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
      if((gameManager.followers.Count == unlockReq) && !created) {
        cam.SetTarget(transform, true);
        created = true;
        StartCoroutine(Create());
      }
    }

    IEnumerator Create() {
      while(cam.arrived == false) {
        yield return null;
      }
      GameObject g = (GameObject)Instantiate(createPrefab, transform.position, Quaternion.identity);
      sound.Play();
      g.transform.parent = transform;
      g.layer = 8;
      g.transform.Rotate(0, 0, worldRotate.currentRotation, Space.World);
      GameObject p = (GameObject)Instantiate(createParticle, transform.position, Quaternion.identity);
      p.transform.parent = transform;
      p.transform.Rotate(0, 0, worldRotate.currentRotation, Space.World);
      Destroy(p, 5f);
      yield return new WaitForSeconds(3);
      cam.SetTarget(gameManager.player.transform, true);
    }
}
