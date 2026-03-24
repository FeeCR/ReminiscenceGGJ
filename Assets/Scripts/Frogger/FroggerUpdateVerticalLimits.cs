using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerUpdateVerticalLimits : MonoBehaviour
{
    public bool smoothMove;
    public float moveOnY;
    private FroggerPlayerController playerController;
    public GameObject camera;
    public bool enableUpdate;
    public GameObject downLimit;

    private Vector3 defaultCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("FroggerPlayer").GetComponent<FroggerPlayerController>();
        enableUpdate = true;
        defaultCameraPos = new Vector3(0, 4.5f, -10);
        ResetLimits();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCameraUp()
    {
        if (!enableUpdate)
            return;

        Vector3 toPosition = new Vector3(camera.transform.position.x, camera.transform.position.y + moveOnY, camera.transform.position.z);
        enableUpdate = false;
        if (smoothMove)
        {
            StartCoroutine(MoveCamera(toPosition));
        } else
        {
            TransformCamera(toPosition);
        }
    }

    private IEnumerator MoveCamera(Vector3 endPos)
    {
        Vector3 startPos = camera.transform.position;
        float elapsed = 0f;
        float duration = 1f;

        playerController.controlEnabled = false;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            camera.transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        camera.transform.position = endPos;
        downLimit.transform.position = new Vector3(downLimit.transform.position.x, playerController.transform.position.y - 1, downLimit.transform.position.z);
        playerController.controlEnabled = true;
    }

    private void TransformCamera(Vector3 endPos)
    {
        camera.transform.position = endPos;
        downLimit.transform.position = new Vector3(downLimit.transform.position.x, playerController.transform.position.y - 1, downLimit.transform.position.z);
    }

    public void ResetLimits()
    {
        var updaters = FindObjectsOfType<FroggerUpdateVerticalLimits>();
        foreach (FroggerUpdateVerticalLimits updater in updaters)
        {
            updater.enableUpdate = true;
        }
        TransformCamera(defaultCameraPos);
    }

}
