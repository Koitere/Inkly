using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetect : MonoBehaviour
{
  private WorldRotate worldRotate;
  private GameManager gameManager;
  private Vector3 pos;
  public bool right;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameInstance;
        worldRotate = WorldRotate.worldRotate;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c) {
      if(c.tag != "Player") {
        return;
      }
      if(right && gameManager.moving == false) {
        pos = gameManager.player.transform.position;
        worldRotate.Rotate(pos, 1, false);
      } else if (!right && gameManager.moving == false) {
        pos = gameManager.player.transform.position;
        worldRotate.Rotate(pos, -1, false);
      }

    }
}
