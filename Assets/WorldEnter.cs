using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEnter : MonoBehaviour
{
  public string levelToLoad = "0";

  public SceneFader sceneFader;
  bool withinRange;

  public void Play()
  {
    sceneFader.FadeTo(levelToLoad);
  }

  void Update()
  {
    if(withinRange) {
      if(Input.GetKeyDown("e")) {
        Play();
      }
    }
  }

  void OnTriggerEnter(Collider c) {
    if(c.tag != "Player") {
      return;
    }
    withinRange = true;

  }

  void OnTriggerExit(Collider c) {
    if(c.tag != "Player") {
      return;
    }
    withinRange = false;

  }

}
