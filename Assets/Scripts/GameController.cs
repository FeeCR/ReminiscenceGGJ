using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    List<DoorController> doorControllerList;

    [SerializeField]
    List<ArcadeController> arcadeControllerList;

    int testNumb = 0;

    [SerializeField]
    GameObject interactCursor, finalBlocker;

    [SerializeField]
    CameraRef cameraReference;

    [SerializeField]
    Player playerController;

    [SerializeField]
    PlayerCamera playerCam;

    [SerializeField]
    List<Transform> arcadeCameraPositions;

    [SerializeField]
    FroggerPlayerController froggerPlayerController;

    [SerializeField]
    SpaceInvadersPlayerController spaceInvadersController;

    [SerializeField]
    PlayerDK playerDk;

    [SerializeField]
    Transform cameraObj;

    [SerializeField]
    float rangeToInteract;

    [SerializeField]
    LayerMask layerMask;

    bool onArcade;
    int? actualArcade;

    private void Update()
    {
        CheckInteraction();
        
        /*
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TriggerDoor(testNumb);

            testNumb++;
        }
        */

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (onArcade)
            {
                MoveCameraToPlayer();
            }
        }
    }

    public void TriggerDoor(int door)
    {
        doorControllerList[door].OpenDoor();
    }

    public void ToggleInteract(bool show)
    {
        if (onArcade)
        {
            interactCursor.SetActive(false);
        }


        interactCursor.SetActive(show);
    }

    public void MoveCameraToArcade(int arcadeNumber)
    {
        if (onArcade)
            return;

        onArcade = true;
        actualArcade = arcadeNumber;

        DelegatePlayerControl(false);

        playerCam.TeleportToPosition(arcadeCameraPositions[arcadeNumber]);

        SwitchArcadeControl(true);
    }

    void SwitchArcadeControl(bool shouldControl)
    {
        switch (actualArcade)
        {
            case 0:
                Debug.Log("Deactivating frog");
                froggerPlayerController.controlEnabled = shouldControl;
                break;

            case 1:
                spaceInvadersController.controlEnabled = shouldControl;
                break;

            case 2:
                playerDk.SetControlEnabled(shouldControl);
                break;

            default:
                Debug.Log("no arcade");
                break;
        }

        if (shouldControl == false)
            actualArcade = null;
    }

    void DelegatePlayerControl(bool shouldControl)
    {
        cameraReference.SetCameraOwner(shouldControl);
        playerController.DelegatePlayerControl(shouldControl);
        playerCam.DelegatePlayerControl(shouldControl);

    }

    public void CompletedArcade(int arcadeId)
    {
        arcadeControllerList[arcadeId].CompletedArcade();

        if(onArcade)
            MoveCameraToPlayer();

        HandleNextStep(arcadeId);
    }

    void HandleNextStep(int arcadeId)
    {
        switch (arcadeId)
        {
            case 0:
                TriggerDoor(1);
                break;

            case 1:
                TriggerDoor(2);
                break;

            case 2:
                finalBlocker.SetActive(false);
                break;

            default:
                Debug.Log("no arcade");
                break;
        }
    }

    public void MoveCameraToPlayer()
    {
        onArcade = false;

        playerCam.TeleportToPosition(playerCam.transform.parent);

        SwitchArcadeControl(false);

        DelegatePlayerControl(true);
    }

    void CheckInteraction()
    {
        RaycastHit hit;

        bool canInteract;

        if (Physics.Raycast(cameraObj.position, cameraObj.forward, out hit, rangeToInteract, layerMask))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MoveCameraToArcade(hit.collider.GetComponent<InteractableObject>().GetTagNumber());
                }

                canInteract = true;
            }
            else
            {

                canInteract = false;
            }

            ToggleInteract(canInteract);
        }
    }
}
