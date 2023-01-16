using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

  /*public GameObject player;
  public float targetDistance;
  public float allowedDistance = 2;
  public GameObject follower;
  public float followSpeed;
  public RaycastHit shot;


    // Update is called once per frame
    void Update()
    {
      transform.LookAt(player.transform);
      if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot)) {
        targetDistance = shot.distance;
        if(targetDistance >= allowedDistance) {
          followSpeed = 0.5f;
          Vector3 adjusted = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
          transform.position = Vector3.MoveTowards(transform.position, adjusted, followSpeed);
        } else {
          followSpeed = 0;
        }
      }

    }
}*/

float speed = 10.0f;
float rotationSpeed = 5.0f;
float minFollowRange = 30.0f;
float dontComeCloserRange = 4.0f;
float waitRange = 10.0f;
public Transform target;

int damage = 1;
private float lastHitTime = 0.0f;


void Start() {

 // Auto setup player as target through tags
 if (target == null && GameObject.FindWithTag("Player"))
     target = GameObject.FindWithTag("Player").transform;
     Patrol();

}


void ReConnect()
{
if (GameObject.FindWithTag("Player"))
Debug.Log("Reconnecting...");
target = GameObject.FindWithTag("Player").transform;
}


void Patrol () {

  Vector3 curPlayerPos = target.position;
     if (CanSeeTarget ())
       StartCoroutine(FollowPlayer());
}

bool CanSeeTarget () {
    if (target != null)
      return true;
    return false;
    }


IEnumerator AIBOPlay () {
  var direction = target.position - transform.position;
  direction.y = 0;
  float distance = Vector3.Distance(transform.position, target.position);
  // Rotate towards the target
  while (distance < dontComeCloserRange * 1.2) {
  //Delay following player until dontComeCloserRange time 2 - solved a stutter problem
  if (distance > dontComeCloserRange * 1.2)
    StartCoroutine(FollowPlayer());
  yield return null;
}
}

IEnumerator FollowPlayer() {
    //Start The Chase
    while (target != null) {

      float distance = Vector3.Distance(transform.position, target.position);
      Vector3 lastVisiblePlayerPosition = target.position;
      if(distance > dontComeCloserRange) {
        lastVisiblePlayerPosition = target.position;
        MoveTowards (lastVisiblePlayerPosition);
      } else {
      }

     yield return null;
     }

}


void MoveTowards (Vector3 position) {
  Vector3 direction = position - transform.position;
  // Rotate towards the target
  //transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
  //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
  // Modify speed so we slow down when we are not facing the target
  Vector3 forward = transform.TransformDirection(Vector3.forward);
  float speedModifier = Vector3.Dot(forward, direction.normalized);
  speedModifier = Mathf.Clamp01(speedModifier);
  // Move the character
  float distance = Vector3.Distance(transform.position, target.position);
  if (distance > minFollowRange) {
    speed = 12.0f;
  } else if (distance < waitRange){
    speed = 5.0f;
  } else {
    speed = 9.0f;
  }
  //direction = forward * speed * speedModifier;
  this.GetComponent<CharacterController>().Move(direction * Time.deltaTime);

}


void RotateTowards (Vector3 position) {
  Vector3 direction = position - transform.position;
  direction.y = 0;
  if (direction.magnitude < 0.1)
    return;
  // Rotate towards the target
  transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
  transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
}
}
