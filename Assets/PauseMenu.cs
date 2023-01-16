using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  public GameObject ui;
  public SceneFader sceneFader;
  public string menuSceneName = "MainMenu";

    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
      {
        Toggle();
      }
    }

    public void Toggle()
    {
      ui.SetActive(!ui.activeSelf);
      if(ui.activeSelf)
      {
        Time.timeScale = 0f; //set Time.fixedDeltaTime if doing slow-mo or something but not needed if just pausing
        Cursor.lockState = CursorLockMode.None;
      } else
      {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
      }
    }

    public void Menu()
    {
      Toggle();
      sceneFader.FadeTo(menuSceneName);
      Cursor.lockState = CursorLockMode.None;
    }

    public void Retry()
    {
      Toggle();
      sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

}
