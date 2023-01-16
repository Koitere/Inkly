using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPiece : MonoBehaviour
{
  private GameManager gameManager;
  public float hoverRate = 0.004f;
  public int levelID;
  public float maxX = -1000f;
  public float minX = 1000f;
  public int pieceID;
  public Image display;
  public Sprite collected;
  private float timeElapsed = 0f;
  AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
      gameManager = GameManager.gameInstance;
      sound = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider c) {
      if(c.tag != "Player") {
        return;
      }
      gameManager.CollectPiece(levelID, pieceID);
      sound.Play();
      StartCoroutine(gameManager.displayMessage("Art Piece Collected!"));
      transform.position = new Vector3(0,0,-30f);
      display.sprite = collected;
      Destroy(this.gameObject, 5f);

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
