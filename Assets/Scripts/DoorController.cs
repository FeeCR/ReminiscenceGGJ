using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    Transform pivot;

    [SerializeField]
    GameObject doorBlocker;

    float animTime = 1f;

    float maxRot = 169;

    public void OpenDoor()
    {
        StartCoroutine("OpenDoor_CO");
    }

    IEnumerator OpenDoor_CO()
    {
        GetComponent<AudioSource>().Play();

        float t = 0;

        while (t < animTime)
        {
            pivot.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, maxRot, t/animTime));

            t += Time.deltaTime;
            yield return null;
        }

        doorBlocker.SetActive(false);
    }
}
