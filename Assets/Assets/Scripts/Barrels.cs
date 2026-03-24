using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrels : MonoBehaviour
{
    public GameObject prefab;
    public float mintime = 2f;
    public float maxtime = 4f;
    private void Start()
    {

        Spawn();

    }

    // Update is called once per frame
    private void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        Invoke(nameof(Spawn), Random.Range(mintime, maxtime));
    }
}
