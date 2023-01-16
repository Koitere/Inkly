using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public bool moving = false;
  public bool bitSwitch = false;
  public List<GameObject> followers;
  public static GameManager gameInstance;
  public SceneFader sceneFader;
  public GameObject followerPrefab;
  public GameObject player;
  public GameObject followerObject;
  public bool[,] collected;
  public Text messageDisplay;


    // Start is called before the first frame update
    void Awake()
    {
      if(gameInstance != null)
      {
        Debug.LogError ("More than one GameManager in scene!");
        return;
      }
      gameInstance = this;
      followers = new List<GameObject>();
      collected = new bool[2,4];
    }

    public void RegisterFollower(GameObject g) {
      Destroy(g);
      GameObject create = (GameObject)Instantiate(followerPrefab, player.transform.position, Quaternion.identity);
      followers.Add(create);

      create.transform.parent = followerObject.transform;

      Follower f = create.GetComponent<Follower>();
      if(followers.Count > 1) {
        f.SetFollowing(followers[followers.Count - 2]);
      } else {
        f.SetFollowing(player);
      }
    }

    public void CollectPiece(int levelID, int pieceID) {
      collected[levelID, pieceID] = true;
      bool levelComplete = true;
      for(int i = 0; i < 4; i++) {
        if(collected[levelID, i] == false) {
          levelComplete = false;
        }
      }
      if(levelComplete) {
        sceneFader.FadeTo("menu");
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(bitSwitch) {
        GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");

        foreach (GameObject trigger in triggers)
        {
          TriggerDetect w = trigger.GetComponent<TriggerDetect>() as TriggerDetect;
          w.right = !w.right;
        }
        bitSwitch = false;
      }
    }

    public void ResetFollowers() {
      foreach(GameObject g in followers) {
        g.transform.position = player.transform.position;
      }
    }

    public IEnumerator displayMessage(string text) {
      messageDisplay.text = text;
      yield return new WaitForSeconds(3);
      messageDisplay.text = "";
    }
}
