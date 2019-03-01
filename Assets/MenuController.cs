using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
  public class MenuController : MonoBehaviour
  {
    public void StartGame()
    {
      Scene firstScene = SceneManager.GetSceneAt(1);
      SceneManager.LoadScene(firstScene.buildIndex);
    }

    public void Quit()
    {
      Application.Quit();
    }
  }
}