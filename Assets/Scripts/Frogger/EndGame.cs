using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    [SerializeField]
    GameController gameController;

    public void FroggerArcadeCompleted()
    {
        gameController.CompletedArcade(0);
    }
}
