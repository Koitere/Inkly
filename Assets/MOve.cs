using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOve : MonoBehaviour
{
  public float moveSpeed = 15;
  private Vector3 moveDir;
  private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetButtonDown("Jump")) {
          velocity.y = Mathf.Sqrt(10 * -2f);
        }
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), velocity.y, 0);
    }

    void FixedUpdate() {
      GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }
}
