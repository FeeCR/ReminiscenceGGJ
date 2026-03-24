using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{
    public UnityEvent EventOnCollide;

    [SerializeField]
    Transform objectToCollide;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == objectToCollide)
        {
            EventOnCollide.Invoke();
            Destroy(this.gameObject);
        }
    }
}

