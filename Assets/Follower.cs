using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
  public GameObject following;
  public float distance;
  private GameManager gameManager;
  public GameObject endJoint;
    // Start is called before the first frame update
    void Start()
    {
      gameManager = GameManager.gameInstance;
    }

    // Update is called once per frame
    void Update()
    {
      distance = Vector3.Distance(transform.position, following.transform.position);
      if(distance > 10) {
        this.transform.position = following.transform.position;
        //gameManager.ResetFollowers();
      }
    }

    public void SetFollowing(GameObject g) {
      following = g;
      ConfigurableJoint j = endJoint.GetComponent<ConfigurableJoint>();
      Rigidbody r = following.GetComponent<Rigidbody>();
      j.connectedBody = r;
    }
}
