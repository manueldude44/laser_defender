using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLoopController : MonoBehaviour
{
  [SerializeField] GameObject[] _enemeyPrefabs;
  [SerializeField] private int _level = 0;
  [SerializeField] private int _enemiesPerLevel = 0;

  public static Vector2 ENEMY_PADDING = new Vector2(1f, 2f);
  public static float ENEMY_SPEED = 1f;
  private readonly Vector3[] ENEMY_COLUMN_POSITIONS = {
    new Vector3(0.2f, 1),
    new Vector3(0.4f, 1),
    new Vector3(0.6f, 1),
    new Vector3(0.8f, 1),
    new Vector3(1, 1),
  };

  private List<GameObject> _enemies = new List<GameObject>();

  void Start()
  {
    BeginNewLevel();
  }

  private void BeginNewLevel()
  {
    // Increment level and monster count.
    _level++;
    _enemiesPerLevel = _enemiesPerLevel == 0 ? 5 : _enemiesPerLevel + 1;

    // Reset existing gameobjects.
    _enemies.ForEach(x => Destroy(x.gameObject));
    _enemies = new List<GameObject>();

    // Setup level requirements.
    SpawnEnemies();
  }

  void Update()
  {
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
      var adjustedLevelSpeed = _level * 0.5f;
      enemy.transform.position = new Vector3(
        enemy.transform.position.x,
        enemy.transform.position.y - (ENEMY_SPEED * adjustedLevelSpeed * Time.deltaTime)
      );
    }
  }

  private void SpawnEnemies()
  {
    var gameCamera = Camera.main;
    var numberOfEnemies = _enemiesPerLevel;
    Random.InitState(_level);

    for (int i = 0; i < numberOfEnemies; i++)
    {
      // Create and name enemy. Only 2 enemies exist right now so 
      // we hard code index.
      var randomEnemyIndex = Random.Range(0f, 10f) > 5f ? 1 : 0;
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
}
