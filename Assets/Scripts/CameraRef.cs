using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRef : MonoBehaviour
{
    [SerializeField]
    Transform cameraReference;

    bool shouldUpdateCameraPos = true;

    private void FixedUpdate()
    {
        if (shouldUpdateCameraPos)
        {
            if (cameraReference != null) transform.position = cameraReference.position;
        }
    }

    public void SetCameraOwner(bool shouldSetToPlayer)
    {
        shouldUpdateCameraPos = shouldSetToPlayer;
    }
}
