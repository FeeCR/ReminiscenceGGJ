using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    float sensX, sensY;

    [SerializeField]
    Transform orientation;

    private float xRotation, yRotation;

    bool shouldControlCamera = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!shouldControlCamera)
            return;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void TeleportToPosition(Transform transformToGo)
    {
        StartCoroutine("GoToTransform_CO", transformToGo);
    }

    IEnumerator GoToTransform_CO(Transform transformToGo)
    {
        float t = 0, animTime = 1;

        Vector3 position = transform.position;

        transform.rotation = transformToGo.rotation;

        while (t < animTime)
        {
            transform.position = Vector3.Lerp(position, transformToGo.position, t / animTime);
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void DelegatePlayerControl(bool canControl)
    {
        shouldControlCamera = canControl;
    }
}
