using UnityEngine;
using UnityEngine.UI;

public class GameInfoText : MonoBehaviour
{
  private Text textInfo;
  private void Awake()
  {
    textInfo = gameObject.GetComponent<Text>();
  }

  void Update()
  {
    textInfo.text = string.Format(
      "Level {0} - Score: {1}",
      GameState.instance.CurrentLevel,
      GameState.instance.CurrentScore
    );
  }
}
