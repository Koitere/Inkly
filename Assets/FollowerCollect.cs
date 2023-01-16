using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCollect : MonoBehaviour
{
  private GameManager gameManager;
  public float hoverRate = 0.004f;
  private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
      gameManager = GameManager.gameInstance;
    }

    void OnTriggerEnter(Collider c) {
      if(c.tag != "Player") {
        return;
      }
      gameManager.RegisterFollower(gameObject);


    }

    void Update()
    {

      if(!gameManager.moving) {

        timeElapsed += Time.deltaTime;
        transform.position += (Vector3.up * hoverRate * Mathf.Sin(timeElapsed));

      } else {
        timeElapsed = 0;
        transform.position = transform.parent.transform.position;
      }
    }
}
