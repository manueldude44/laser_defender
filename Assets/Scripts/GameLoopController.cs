using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoopController : MonoBehaviour
{
  [SerializeField] private GameObject[] _enemeyPrefabs = new GameObject[2];
  private int _enemiesPerLevel = 0;

  public static GameLoopController instance = null;
  public static Vector2 ENEMY_PADDING = new Vector2(1f, 2f);
  public static float ENEMY_SPEED = 1f;
  private const int INITIAL_ENEMY_COUNT = 5;
  private readonly Vector3[] ENEMY_COLUMN_POSITIONS = {
    new Vector3(0.2f, 1),
    new Vector3(0.4f, 1),
    new Vector3(0.6f, 1),
    new Vector3(0.8f, 1),
    new Vector3(1, 1),
  };
  private const float ENEMY_Z_LAYER = 9f;

  private List<GameObject> _enemies = new List<GameObject>();

  private void Awake()
  {
    instance = this;
  }

  void Start()
  {
    BeginNewLevel();
  }

  private void BeginNewLevel()
  {
    // Increment level and monster count.
    GameState.instance.IncrementLevel();
    _enemiesPerLevel = _enemiesPerLevel == 0 ? INITIAL_ENEMY_COUNT : _enemiesPerLevel + 1;

    // Reset existing gameobjects.
    _enemies.ForEach(x => Destroy(x.gameObject));
    _enemies = new List<GameObject>();

    // Setup level requirements.
    SpawnEnemies();
  }

  void Update()
  {
    HandleUpdatingEnemies();
  }

  private void HandleUpdatingEnemies()
  {
    // Handle enemy interactions.
    var enemiesStillAlive = _enemies.Where(x => x.activeSelf);

    // Start a new round if no enemies are left.
    if (!enemiesStillAlive.Any())
    {
      BeginNewLevel();
      return;
    }

    // If enemies exist, update their locations
    foreach (var enemy in enemiesStillAlive)
    {
      var adjustedLevelSpeed = GameState.instance.CurrentLevel * 0.5f;
      enemy.transform.position = new Vector3(
        enemy.transform.position.x,
        enemy.transform.position.y - (ENEMY_SPEED * adjustedLevelSpeed * Time.deltaTime),
        ENEMY_Z_LAYER
      );
    }
  }

  private void SpawnEnemies()
  {
    var gameCamera = Camera.main;
    var numberOfEnemies = _enemiesPerLevel;
    UnityEngine.Random.InitState(GameState.instance.CurrentLevel);

    for (int i = 0; i < numberOfEnemies; i++)
    {
      // Create and name enemy. Only 2 enemies exist right now so 
      // we hard code index.
      var randomEnemyIndex = UnityEngine.Random.Range(0f, 10f) > 5f ? 1 : 0;
      var enemy = Instantiate(_enemeyPrefabs[randomEnemyIndex]);
      _enemies.Add(enemy);
      enemy.name = i.ToString();

      // Calculate indexes.
      var rowOffset = i / ENEMY_COLUMN_POSITIONS.Length;
      var columnOffset = ENEMY_COLUMN_POSITIONS[i % ENEMY_COLUMN_POSITIONS.Length];

      // Set enemy position.
      var enemyPosition = gameCamera.ViewportToWorldPoint(columnOffset);
      enemy.transform.position = new Vector3(enemyPosition.x - ENEMY_PADDING.x, enemyPosition.y - ENEMY_PADDING.y - rowOffset);
    }
  }

  #region Public Methods
  public void PrepareGameOver()
  {
    _enemies.ForEach(x => x.SetActive(false));
    SceneManager.LoadScene(SceneConstants.GAME_OVER);
  }

  public void PrepareGameReset()
  {
    SceneManager.LoadScene(SceneConstants.BATTLEFIELD);
    BeginNewLevel();
  }
  #endregion
}
