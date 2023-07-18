using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private int _cubeCount = 1;
    [SerializeField] private Transform _playerPlatformPrefab;
    [SerializeField] private Transform _stickman;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _cubeHolder;
    [SerializeField, Range(0, 0.1f)] private float _cubesOffset;
    [SerializeField] private GameObject _cubePrefab;
    private int _jumpHash;

    private float _playersCubeHeight;

    private void Awake()
    {
        _jumpHash = Animator.StringToHash("Jump");
        _playersCubeHeight = _playerPlatformPrefab.localScale.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            _stickman.position += Vector3.up * (_playersCubeHeight + _cubesOffset);
            _cubeHolder.position += Vector3.up * (_playersCubeHeight + _cubesOffset);
            Transform cube = Instantiate(_cubePrefab, Vector3.zero, Quaternion.identity, _cubeHolder).transform;
            cube.localPosition = -Vector3.up * (_playersCubeHeight + _cubesOffset) * _cubeCount;
            _animator.SetTrigger(_jumpHash);
            _cubeCount += 1;
        }
    }

}
