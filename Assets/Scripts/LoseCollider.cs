using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
  private void OnTriggerStay2D(Collider2D collision)
  {
    GameSceneManager.LoadGameOver();
  }
}
