using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteractor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            transform.parent.SetParent(null);
            CameraShakeController.Instance.ShakeCamera(2, 0.3f);
        }
        else if (other.CompareTag("CubeDestroy"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
