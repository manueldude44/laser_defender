using UnityEngine;

public class Missile : MonoBehaviour
{
  [SerializeField] float _maxY;

  float _offWorldPadding = 1f;

  // Start is called before the first frame update
  void Start()
  {
    Camera gameCamera = Camera.main;
    _maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1)).y + _offWorldPadding;
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.position.y > _maxY) Destroy(gameObject);
  }
}
