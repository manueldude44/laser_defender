using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    float _offWorldPadding = 1f;
    [SerializeField] float _maxY;

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
