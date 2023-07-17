using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MeshRenderer _platformFloor;
    [SerializeField] private MeshRenderer _playerPlatformPrefab;
    [SerializeField] private float sensivity = 1f;
    private float _platformWidth;
    private float _playersCubeWidth = 0.5f;

    private void Awake()
    {
        _platformWidth = _platformFloor.bounds.extents.x;
        _playersCubeWidth = _playerPlatformPrefab.bounds.extents.x;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                float newXPosition = transform.position.x - touchDeltaPosition.x * sensivity * Time.deltaTime;
                float clampedXPosition = Mathf.Clamp(newXPosition, -_platformWidth + _playersCubeWidth, _platformWidth - _playersCubeWidth);
                transform.position = new Vector3(clampedXPosition, transform.position.y, transform.position.z);
            }
        }
    }
}

