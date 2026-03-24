using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    [SerializeField]
    int tagNumber;

    public int GetTagNumber()
    {
        return tagNumber;
    }
}
