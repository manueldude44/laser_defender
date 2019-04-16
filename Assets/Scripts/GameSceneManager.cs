using UnityEngine.SceneManagement;

public class GameSceneManager
{
  public static void LoadGameOver()
  {
    GameLoopController.instance.PrepareGameOver();
  }

  public static void LoadBattlefield()
  {
    SceneManager.LoadScene(SceneConstants.BATTLEFIELD);
  }
}