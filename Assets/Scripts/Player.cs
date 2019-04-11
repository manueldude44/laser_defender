using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  [SerializeField] float _moveSpeed = 20f;
  [SerializeField] float _spritePadding = 1f;
  [SerializeField] float _projectileSpeed = 45f;
  [SerializeField] float _projectileWaitTimer = 0.1f;
  [SerializeField] public GameObject _laserPrefab;

  float xMin, xMax, yMin, yMax;
  Coroutine _fireRoutine;

  #region Lifecycle methods

  private void Start()
  {
    SetupMoveBoundaries();
  }

  private void Update()
  {
    Move();
    CheckFire();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == TagConstants.ENEMY)
    {
      SceneManager.LoadScene(SceneConstants.GAME_OVER);
    }
  }
  #endregion

  private void Move()
  {
    var deltaX = Input.GetAxis(InputConstants.HORIZONTAL) * Time.deltaTime * _moveSpeed;
    var deltaY = Input.GetAxis(InputConstants.VERTICAL) * Time.deltaTime * _moveSpeed;

    var newX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
    var newY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

    transform.position = new Vector3(newX, newY, transform.position.z);
  }

  private void SetupMoveBoundaries()
  {
    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).x + _spritePadding;
    xMax = gameCamera.ViewportToWorldPoint(new Vector2(1, 0)).x - _spritePadding;

    yMin = gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).y + _spritePadding;
    yMax = gameCamera.ViewportToWorldPoint(new Vector2(0, 1)).y - _spritePadding;

  }

  private void CheckFire()
  {
    if (Input.GetButtonDown(InputConstants.FIRE1))
    {
      _fireRoutine = StartCoroutine(Fire());
    }
    if (Input.GetButtonUp(InputConstants.FIRE1))
    {
      StopCoroutine(_fireRoutine);
    }
  }

  private IEnumerator Fire()
  {
    // Get's stopped by ButtonUp
    while (true)
    {
      GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
      laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _projectileSpeed);
      yield return new WaitForSeconds(_projectileWaitTimer);
    }
  }
}
