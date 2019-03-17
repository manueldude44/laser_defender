using System.Collections.Generic;
using UnityEngine;

public class GameLoopController : MonoBehaviour
{
  private int level = 1;

  public static float ENEMY_SPEED = 1f;
  public static Vector3 ENEMY_DESTINATION;

  private const int ENEMIES_PER_LEVEL = 5;
  [SerializeField] GameObject _enemeyPrefab;
  private readonly Vector3[] ENEMY_ROW_POSITIONS = {
    new Vector3(1, 1),
    new Vector3(0.8f, 1),
    new Vector3(0.4f, 1),
    new Vector3(0.2f, 1),
    new Vector3(0.6f, 1),
    new Vector3(0, 0)
  };

  private List<GameObject> _enemies = new List<GameObject>();
  
  void Start()
  {
    HydrateEnemyDestination();
    SpawnEnemies();
  }

  
  void Update()
  {
    var newLocation = GetNewEnemyLocation(_enemies[0].transform.position);
    foreach (var enemy in _enemies)
    {
      enemy.transform.position = new Vector3(enemy.transform.position.x, newLocation.y);
    }
  }

  private void HydrateEnemyDestination()
  {
    var gameCamera = Camera.main;
    ENEMY_DESTINATION = gameCamera.ViewportToWorldPoint(new Vector3(-0.2f, -0.2f));
  }

  private void SpawnEnemies()
  {
    var gameCamera = Camera.main;
    var numberOfEnemies = level * ENEMIES_PER_LEVEL;
    var row = 0;
    var enemyPadding = 1f;

    for (int i = 0; i < numberOfEnemies; i++)
    {
      var enemy = Instantiate(_enemeyPrefab);
      var columnPosition = ENEMY_ROW_POSITIONS[i % ENEMY_ROW_POSITIONS.Length];
      var enemyPosition = gameCamera.ViewportToWorldPoint(columnPosition);
      enemy.transform.position = new Vector3(enemyPosition.x - enemyPadding, enemyPosition.y - enemyPadding);
      _enemies.Add(enemy);
    }
  }

  private Vector3 GetNewEnemyLocation(Vector3 enemyPosition)
  {
    var delta = Vector2.MoveTowards(
      new Vector2(enemyPosition.x, enemyPosition.y),
      ENEMY_DESTINATION,
      ENEMY_SPEED * Time.deltaTime
    );

    return new Vector3(transform.position.x, delta.y);
  }
}
