using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private List<Transform> _activePlatforms;
    [SerializeField] private List<Transform> _inactivePlatforms;
    [SerializeField] private float _platformSpeed = 1f;
    [SerializeField] private GameObject _platformPrefabs;
    [SerializeField] private Transform _platformFloor;
    private float _platformLength;

    private void Awake()
    {
        _platformLength = _platformFloor.localScale.z;
        _activePlatforms = new List<Transform>();
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            _activePlatforms.Add(transform.GetChild(i));
        }
    }

    private void Update()
    {
        foreach (Transform _platform in _activePlatforms)
        {
            _platform.position += Vector3.forward * _platformSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            if (_activePlatforms.Count == 0) throw new UnityException("Active platforms is empty. Unable to calculate platform position.");
            Vector3 platformPosition = (_activePlatforms[_activePlatforms.Count - 1].position.z - _platformLength) * Vector3.forward;
            Transform platform = GetPlatformFromInactiveList(platformPosition);
            if (_platformPrefabs != null && platform == null)
            {
                platform = Instantiate(_platformPrefabs, platformPosition, Quaternion.identity, transform).transform;
            }

            DeactivateActivePlatform(_activePlatforms[0]);
            MovePlatformToActiveList(platform);
        }
    }

    private Transform GetPlatformFromInactiveList(Vector3 position)
    {
        if (_inactivePlatforms.Count > 0)
        {
            Transform platform = _inactivePlatforms[0];
            platform.position = position;
            platform.gameObject.SetActive(true);
            _inactivePlatforms.RemoveAt(0);
            return platform;
        }
        return null;
    }

    private void DeactivateActivePlatform(Transform platform)
    {
        platform.gameObject.SetActive(false);
        _inactivePlatforms.Add(platform);
        _activePlatforms.RemoveAt(0);
    }

    private void MovePlatformToActiveList(Transform platform)
    {
        _activePlatforms.Add(platform);
    }
}