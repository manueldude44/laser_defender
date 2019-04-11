using UnityEngine;

public class Enemy : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag != TagConstants.PLAYER_BULLET)
    {
      return;
    }

    gameObject.SetActive(false);
    Destroy(collision.gameObject);
  }
}
