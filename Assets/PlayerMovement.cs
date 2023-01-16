using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public CharacterController controller;
  public float speed = 12f;
  public float gravity = -9.81f;
  public float jumpHeight = 3f;

  public Transform groundCheck;
  public float groundDistance = 1f;
  public LayerMask groundMask;
  public Camera cam;
  public Follower follower;
  private float x = 0;

  Vector3 velocity;
  public bool isGrounded;
  public bool doubleJump = true;
  public bool moving = false;
  public bool facingRight = true;
  public AudioSource jump;
  Animator anim;

  void Start() {
    anim = gameObject.GetComponent<Animator>();
  }

  void Update()
  {
    if(Input.GetKey("f")) {
      cam.orthographicSize = 70;
    } else {
      cam.orthographicSize = 20;
    }
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    if(isGrounded && velocity.y < 0)
    {
      velocity.y = -2f;
      doubleJump = true;
    }

    x = Input.GetAxis("Horizontal") * speed;

    anim.SetFloat("speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

    Vector3 move = transform.right * x;



    controller.Move(move * Time.deltaTime);
    if( x > 0 && !facingRight) {
      Flip();
    } else if(x < 0 && facingRight) {
      Flip();
    }

    velocity.y += gravity * Time.deltaTime;

    if(Input.GetButtonDown("Jump") && (isGrounded || doubleJump)) {
      if(!isGrounded) {doubleJump = false;}
      jump.Play();
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    controller.Move(velocity * Time.deltaTime);

  }

  void Flip()
{
    facingRight = !facingRight;
    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
}
}
