using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sensivity = 1f;
    [SerializeField] private Transform _platformFloor;
    [SerializeField] private Transform _playerPlatformPrefab;
    private float _platformWidthOffset;
    private float _playersCubeWidthOffset;

    private void Awake()
    {

        _platformWidthOffset = _platformFloor.localScale.x * 0.5f;
        _playersCubeWidthOffset = _playerPlatformPrefab.localScale.x * 0.5f;
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
                float clampedXPosition = Mathf.Clamp(newXPosition, -_platformWidthOffset + _playersCubeWidthOffset, _platformWidthOffset - _playersCubeWidthOffset);
                transform.position = new Vector3(clampedXPosition, transform.position.y, transform.position.z);
            }
        }
    }
}

