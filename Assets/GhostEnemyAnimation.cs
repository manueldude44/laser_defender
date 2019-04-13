using System.Collections;
using UnityEngine;

public class GhostEnemyAnimation : MonoBehaviour
{
  private bool shouldAnimate = true;
  private const float ROTATION_WAIT_TIME = 0.1F;

  private void Update()
  {
    if (shouldAnimate)
    {
      StartCoroutine(RotationAnimateSprite());
    }
  }

  private IEnumerator RotationAnimateSprite()
  {
    shouldAnimate = false;
    yield return new WaitForSeconds(ROTATION_WAIT_TIME);
    transform.Rotate(0f, 0, 45f);
    shouldAnimate = true;
  }
}
