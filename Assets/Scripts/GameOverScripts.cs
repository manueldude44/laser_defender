using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScripts : MonoBehaviour
{
  public void Restart()
  {
    GameState.instance.StartGame();
  }

  public void Quit()
  {
    GameState.instance.QuitGame();
  }
}
