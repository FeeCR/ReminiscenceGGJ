using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeController : MonoBehaviour
{
    [SerializeField]
    Color standby, done;

    [SerializeField]
    GameObject plane;

    Material instancedMatPlane;
    Light planeLight;

    bool completedArcade = false;

    bool shouldAnim = true;

    private void Start()
    {
        instancedMatPlane = plane.GetComponent<MeshRenderer>().material;
        planeLight = plane.transform.GetChild(0).GetComponent<Light>();

        StartCoroutine(AnimateColor());
    }

    IEnumerator AnimateColor()
    {
        float maxIntensity = 2;

        while (shouldAnim)
        {
            var t = Mathf.PingPong(Time.time, 1);

            instancedMatPlane.color = Color.Lerp(Color.white, standby, t);
            planeLight.intensity = Mathf.Lerp(1, maxIntensity, t);

            yield return null;
        }
    }

    public void CompletedArcade()
    {
        if (completedArcade)
            return;

        completedArcade = true;

        shouldAnim = false;

        instancedMatPlane.color = done;
        planeLight.intensity = 0;
    }
}
