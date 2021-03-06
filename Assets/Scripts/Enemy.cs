﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
  private const int SCORE_PER_ENEMY = 10;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag != TagConstants.PLAYER_BULLET)
    {
      return;
    }

    gameObject.SetActive(false);
    Destroy(collision.gameObject);
    GameState.instance.UpdateScore(SCORE_PER_ENEMY);
  }
}
