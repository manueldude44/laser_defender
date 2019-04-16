using UnityEngine;
using UnityEditor;

public class GameState : MonoBehaviour
{
  public static GameState instance = null;

  private int Level { get; set; }
  private int Score { get; set; }
  public int CurrentLevel => Level;
  public int CurrentScore => Score;

  private void Start()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    } else
    {
      Destroy(gameObject);
    }
  }

  public void IncrementLevel()
  {
    Level++;
  }

  public void UpdateScore(int scoreToAdd)
  {
    Score += scoreToAdd;
  }

  public void StartGame()
  {
    Debug.Log("Starting game over");
    Level = 0;
    Score = 0;
    GameSceneManager.LoadBattlefield();
  }

  public void QuitGame()
  {
    Debug.Log("Quitting Application");
    Application.Quit();
  }
}